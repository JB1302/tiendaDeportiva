using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using tiendaDeportiva.Controllers;
using tiendaDeportiva.Models;
using tiendaDeportivaAPI.Models.Dto;

namespace Productos.API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly DBContextController _context = new DBContextController();

        private int ObtenerCategoriaValor(Producto producto)
        {
            var prop = typeof(Producto).GetProperty("Categoria");

            if (prop == null)
                return 0;

            var valor = prop.GetValue(producto);

            if (valor == null)
                return 0;

            return Convert.ToInt32(valor);
        }

        private void AsignarCategoria(Producto producto, int categoriaValor)
        {
            var prop = typeof(Producto).GetProperty("Categoria");

            if (prop == null || !prop.CanWrite)
                return;

            var tipoPropiedad = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            object valorConvertido;

            if (tipoPropiedad.IsEnum)
            {
                valorConvertido = Enum.ToObject(tipoPropiedad, categoriaValor);
            }
            else
            {
                valorConvertido = Convert.ChangeType(categoriaValor, tipoPropiedad);
            }

            prop.SetValue(producto, valorConvertido);
        }

        private ProductoDto MapProducto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = ObtenerCategoriaValor(producto),
                Activo = producto.Activo
            };
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var productos = _context.Producto
                .ToList()
                .Select(MapProducto)
                .ToList();

            return Ok(productos);
        }

        [HttpGet]
        [Route("activos")]
        public IHttpActionResult GetActivos()
        {
            var productos = _context.Producto
                .Where(p => p.Activo)
                .ToList()
                .Select(MapProducto)
                .ToList();

            return Ok(productos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var producto = _context.Producto
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return Ok(MapProducto(producto));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(ProductoUpsertDto input)
        {
            if (input == null)
                return BadRequest("Datos inválidos");

            if (string.IsNullOrWhiteSpace(input.Nombre))
                return BadRequest("El nombre es obligatorio");

            if (input.Precio <= 0)
                return BadRequest("Precio inválido");

            if (input.Stock < 0)
                return BadRequest("Stock inválido");

            var producto = new Producto
            {
                Nombre = input.Nombre,
                Descripcion = input.Descripcion,
                Precio = input.Precio,
                Stock = input.Stock,
                Activo = true
            };

            AsignarCategoria(producto, input.Categoria);

            _context.Producto.Add(producto);
            _context.SaveChanges();

            return Content(HttpStatusCode.Created, MapProducto(producto));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, ProductoUpsertDto input)
        {
            if (input == null)
                return BadRequest("Datos inválidos");

            var producto = _context.Producto.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(input.Nombre))
                return BadRequest("El nombre es obligatorio");

            if (input.Precio <= 0)
                return BadRequest("Precio inválido");

            if (input.Stock < 0)
                return BadRequest("Stock inválido");

            producto.Nombre = input.Nombre;
            producto.Descripcion = input.Descripcion;
            producto.Precio = input.Precio;
            producto.Stock = input.Stock;

            AsignarCategoria(producto, input.Categoria);

            _context.SaveChanges();

            return Ok(MapProducto(producto));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            producto.Activo = false;
            _context.SaveChanges();

            return Ok(new { mensaje = "Producto eliminado" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}