using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using tiendaDeportivaAPI.Models;

namespace Productos.API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: api/productos
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(
            string categoria = null,
            decimal? precioMin = null,
            decimal? precioMax = null,
            int pagina = 1,
            int tamanioPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanioPagina < 1) tamanioPagina = 10;

            IQueryable<Producto> query = _context.Productos.Where(p => p.Activo);

            if (!string.IsNullOrWhiteSpace(categoria))
                query = query.Where(p => p.Categoria == categoria);

            if (precioMin.HasValue)
                query = query.Where(p => p.Precio >= precioMin.Value);

            if (precioMax.HasValue)
                query = query.Where(p => p.Precio <= precioMax.Value);

            var total = query.Count();

            var data = query
                .OrderBy(p => p.Id)
                .Skip((pagina - 1) * tamanioPagina)
                .Take(tamanioPagina)
                .ToList();

            return Ok(new
            {
                total,
                pagina,
                tamanioPagina,
                data
            });
        }

        // GET: api/productos/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var producto = _context.Productos
                .FirstOrDefault(p => p.Id == id && p.Activo);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/productos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest("El producto es requerido.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var errores = new List<string>();
            var categoriasValidas = new[] { "Futbol", "Basquetbol", "Natacion", "Tenis" };

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                errores.Add("nombre obligatorio");

            if (producto.Precio <= 0)
                errores.Add("precio mayor que 0");

            if (producto.Stock < 0)
                errores.Add("stock mayor o igual a 0");

            if (string.IsNullOrWhiteSpace(producto.Categoria) || !categoriasValidas.Contains(producto.Categoria))
                errores.Add("categoria invalida");

            if (errores.Any())
                return Content(System.Net.HttpStatusCode.BadRequest, new { errores });

            producto.Activo = true;

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetProductoPorId",
                new { id = producto.Id },
                producto
            );
        }

        // GET por id con nombre de ruta
        [HttpGet]
        [Route("{id:int}", Name = "GetProductoPorId")]
        public IHttpActionResult GetProductoPorId(int id)
        {
            var producto = _context.Productos
                .FirstOrDefault(p => p.Id == id && p.Activo);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // PUT: api/productos/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest("El producto es requerido.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (existe == null)
                return NotFound();

            var errores = new List<string>();
            var categoriasValidas = new[] { "Futbol", "Basquetbol", "Natacion", "Tenis" };

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                errores.Add("nombre obligatorio");

            if (producto.Precio <= 0)
                errores.Add("precio mayor que 0");

            if (producto.Stock < 0)
                errores.Add("stock mayor o igual a 0");

            if (string.IsNullOrWhiteSpace(producto.Categoria) || !categoriasValidas.Contains(producto.Categoria))
                errores.Add("categoria invalida");

            if (errores.Any())
                return Content(System.Net.HttpStatusCode.BadRequest, new { errores });

            existe.Nombre = producto.Nombre;
            existe.Descripcion = producto.Descripcion;
            existe.Precio = producto.Precio;
            existe.Stock = producto.Stock;
            existe.Categoria = producto.Categoria;

            _context.SaveChanges();

            return Ok(existe);
        }

        // DELETE: api/productos/1
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id, bool logico = true)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            if (logico)
            {
                producto.Activo = false;
            }
            else
            {
                _context.Productos.Remove(producto);
            }

            _context.SaveChanges();

            return Ok(new
            {
                mensaje = logico ? "Producto eliminado logicamente." : "Producto eliminado fisicamente."
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}