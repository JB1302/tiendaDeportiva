using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
using tiendaDeportiva.Models;
using tiendaDeportiva.Models.Enum;
using tiendaDeportivaAPI.Models;
using tiendaDeportivaAPI.Models.Dto;

namespace tiendaDeportiva.Controllers
{
    [RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        private readonly DBContextController _context = new DBContextController();

        private PedidoListaDto MapPedidoLista(Pedido pedido)
        {
            return new PedidoListaDto
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                IdUsuario = pedido.IdUsuario,
                MontoTotal = pedido.MontoTotal,
                Estado = pedido.Estado
            };
        }

        private PedidoDetalleDto MapPedidoDetalle(Pedido pedido)
        {
            return new PedidoDetalleDto
            {
                Id = pedido.Id,
                Fecha = pedido.Fecha,
                IdUsuario = pedido.IdUsuario,
                MontoTotal = pedido.MontoTotal,
                Estado = pedido.Estado,
                Detalles = pedido.Detalles != null
                    ? pedido.Detalles.Select(d => new DetallePedidoDto
                    {
                        IdProducto = d.IdProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Producto = d.Producto != null
                            ? new ProductoDto
                            {
                                Id = d.Producto.Id,
                                Nombre = d.Producto.Nombre
                            }
                            : null
                    }).ToList()
                    : new List<DetallePedidoDto>()
            };
        }

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

            var pedidoGuardado = _context.Pedido
                .Include(p => p.Detalles.Select(d => d.Producto))
                .FirstOrDefault(p => p.Id == pedido.Id);

            return Ok(MapPedidoDetalle(pedidoGuardado));
        }

        [HttpGet]
        [Route("mios")]
        public IHttpActionResult MisPedidos()
        {
            string userId = "demo-user";

            var pedidos = _context.Pedido
                .Where(p => p.IdUsuario == userId)
                .ToList()
                .Select(MapPedidoLista)
                .ToList();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var pedidos = _context.Pedido
                .ToList()
                .Select(MapPedidoLista)
                .ToList();

            return Ok(pedidos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var pedido = _context.Pedido
                .Include(p => p.Detalles.Select(d => d.Producto))
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return Ok(MapPedidoDetalle(pedido));
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

            return Ok(new { mensaje = "Pedido actualizado" });
        }
    }
}