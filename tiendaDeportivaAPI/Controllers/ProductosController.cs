using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Productos.API.Controllers
{
    public class ProductosController : ApiController
    {
        private AppDbContext _context = new AppDbContext();

        // GET: api/productos
        [HttpGet]
        [Route("api/productos")]
        public IHttpActionResult Get(
            string categoria = null,
            decimal? precioMin = null,
            decimal? precioMax = null,
            int pagina = 1,
            int tamanioPagina = 10)
        {
            var query = _context.Productos.Where(p => p.Activo);

            if (!string.IsNullOrEmpty(categoria))
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
        [Route("api/productos/{id}")]
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
        [Route("api/productos")]
        public IHttpActionResult Post([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var errores = new List<string>();
            var categoriasValidas = new[] { "Futbol", "Basquetbol", "Natacion", "Tenis" };

            if (producto == null)
                errores.Add("producto requerido");

            if (string.IsNullOrWhiteSpace(producto?.Nombre))
                errores.Add("nombre obligatorio");

            if (producto?.Precio <= 0)
                errores.Add("precio mayor que 0");

            if (producto?.Stock < 0)
                errores.Add("stock mayor o igual a 0");

            if (!categoriasValidas.Contains(producto?.Categoria))
                errores.Add("categoria invalida");

            if (errores.Any())
                return Content(System.Net.HttpStatusCode.BadRequest, new { errores });

            producto.Activo = true;

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return Ok(producto);
        }

        // PUT: api/productos/1
        [HttpPut]
        [Route("api/productos/{id}")]
        public IHttpActionResult Put(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (existe == null)
                return NotFound();

            var errores = new List<string>();
            var categoriasValidas = new[] { "Futbol", "Basquetbol", "Natacion", "Tenis" };

            if (producto.Precio <= 0)
                errores.Add("precio mayor que 0");

            if (producto.Stock < 0)
                errores.Add("stock mayor o igual a 0");

            if (!categoriasValidas.Contains(producto.Categoria))
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
        [Route("api/productos/{id}")]
        public IHttpActionResult Delete(int id, bool logico = true)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            if (logico)
            {
                producto.Activo = false;
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}