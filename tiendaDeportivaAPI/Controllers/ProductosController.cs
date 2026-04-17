using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using tiendaDeportiva.Controllers;
using tiendaDeportiva.Models;

namespace Productos.API.Controllers
{
    public class ProductosController : ApiController
    {
        private readonly DBContextController _context = new DBContextController();
        [HttpGet]
        public IHttpActionResult Get()
        {
            var productos = _context.Producto
                .Where(p => p.Activo)
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

            return Ok(producto);
        }

        [HttpPost]
        public IHttpActionResult Post(Producto producto)
        {
            if (producto.Precio <= 0)
                return BadRequest("Precio inválido");

            if (producto.Stock < 0)
                return BadRequest("Stock inválido");

            producto.Activo = true;

            _context.Producto.Add(producto);
            _context.SaveChanges();

            return Ok(producto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Producto input)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            producto.Nombre = input.Nombre;
            producto.Descripcion = input.Descripcion;
            producto.Precio = input.Precio;
            producto.Stock = input.Stock;
            producto.Categoria = input.Categoria;

            _context.SaveChanges();

            return Ok(producto);
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

            return Ok("Producto eliminado");
        }
    };
}