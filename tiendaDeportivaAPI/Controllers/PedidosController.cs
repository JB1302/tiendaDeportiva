using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
namespace TiendaDeportiva.API.Controllers
{
    [RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        private readonly TiendaDbContext _db = new TiendaDbContext();

        // POST api/pedidos        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody] CrearPedidoDto dto)
        {
            if (dto == null || dto.Items == null || !dto.Items.Any())
                return BadRequest("Debe incluir un producto");

            if (string.IsNullOrWhiteSpace(dto.IdUsuario))
                return BadRequest("El ID es obligatorio");

            decimal montoTotal = 0;
            var pedido = new Pedido
            {
                IdUsuario = dto.IdUsuario,
                Fecha = DateTime.Now,
                Estado = "Pendiente"
            };

            foreach (var item in dto.Items)
            {
                if (item.Cantidad <= 0)
                    return BadRequest($"Cantidad invalida para producto {item.IdProducto}.");

                var producto = _db.Productos.Find(item.IdProducto);
                if (producto == null || !producto.Activo)
                    return BadRequest($"Producto {item.IdProducto} no existe o esta inactivo");

                if (producto.Stock < item.Cantidad)
                    return BadRequest(
                        $"No hay stock para '{producto.Nombre}'. " +
                        $"Disponible: {producto.Stock}.");

                producto.Stock -= item.Cantidad;

                pedido.Detalles.Add(new DetallePedido
                {
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio
                });

                montoTotal += producto.Precio * item.Cantidad;
            }

            pedido.MontoTotal = montoTotal;
            _db.Pedidos.Add(pedido);
            _db.SaveChanges();

            return Ok(new { pedido.Id, pedido.Fecha, pedido.MontoTotal, pedido.Estado });
        }

        // GET api/pedidos/        [HttpGet, Route("mios")]
        public IHttpActionResult MisPedidos(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return BadRequest("El parametro usuario es obligatorio");

            var pedidos = _db.Pedidos
                .Where(p => p.IdUsuario == usuario)
                .Select(p => new {
                    p.Id,
                    p.Fecha,
                    p.MontoTotal,
                    p.Estado,
                    Detalles = p.Detalles.Select(d => new {
                        d.Cantidad,
                        d.PrecioUnitario,
                        Producto = d.Producto.Nombre
                    })
                })
                .OrderByDescending(p => p.Fecha)
                .ToList();

            return Ok(pedidos);
        }

        // GET api/pedidos         [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var pedidos = _db.Pedidos
                .Select(p => new {
                    p.Id,
                    p.Fecha,
                    p.MontoTotal,
                    p.Estado,
                    p.IdUsuario,
                    CantidadItems = p.Detalles.Count()
                })
                .OrderByDescending(p => p.Fecha)
                .ToList();

            return Ok(pedidos);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
