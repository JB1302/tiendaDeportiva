using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using tiendaDeportiva.Models;
using tiendaDeportiva.Models.Enum;
using tiendaDeportivaAPI.Models;

namespace tiendaDeportiva.Controllers
{
    [RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        private readonly DBContextController _context = new DBContextController();

        [HttpPost]
        [Route("")]
        public IHttpActionResult CrearPedido(PedidoRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest("Pedido vacío");

            var pedido = new Pedido
            {
                Fecha = DateTime.Now,
                Estado = EstadoPedido.Pendiente,
                IdUsuario = "demo-user",
                Detalles = new List<DetallePedido>()
            };

            decimal total = 0;

            foreach (var item in request.Items)
            {
                var producto = _context.Producto
                    .FirstOrDefault(p => p.Id == item.ProductoId);

                if (producto == null)
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        error = $"El producto {item.ProductoId} no existe"
                    });
                }

                if (producto.Stock < item.Cantidad)
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        error = $"Stock insuficiente para {producto.Nombre}"
                    });
                }

                pedido.Detalles.Add(new DetallePedido
                {
                    IdProducto = producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                });

                total += producto.Precio * item.Cantidad;

                producto.Stock -= item.Cantidad;
            }

            pedido.MontoTotal = total;

            _context.Pedido.Add(pedido);
            _context.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("mios")]
        public IHttpActionResult MisPedidos()
        {
            string userId = "demo-user";

            var pedidos = _context.Pedido
                .Where(p => p.IdUsuario == userId)
                .ToList();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var pedidos = _context.Pedido.ToList();
            return Ok(pedidos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var pedido = _context.Pedido
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var pedido = _context.Pedido
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            pedido.Estado = EstadoPedido.Completado;

            _context.SaveChanges();

            return Ok("Pedido actualizado");
        }
    }
}
