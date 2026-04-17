using System;
using System.Linq;
using System.Web.Mvc;
using tiendaDeportiva.Models;

namespace tiendaDeportiva.Controllers
{
    public class CarritoController : Controller
    {
        private DBContextController db = new DBContextController();
        private const string CarritoSessionKey = "CARRITO";

        private Carrito ObtenerCarrito()
        {
            if (Session[CarritoSessionKey] == null)
            {
                Session[CarritoSessionKey] = new Carrito();
            }

            return (Carrito)Session[CarritoSessionKey];
        }

        private void GuardarCarrito(Carrito carrito)
        {
            Session[CarritoSessionKey] = carrito;
        }

        // GET: Carrito
        public ActionResult Index()
        {
            var carrito = ObtenerCarrito();
            return View(carrito);
        }

        [HttpPost]
        public ActionResult Agregar(int idProducto, int cantidad = 1)
        {
            var producto = db.Producto.FirstOrDefault(p => p.Id == idProducto && p.Activo);

            if (producto == null)
            {
                TempData["Error"] = "Producto no encontrado.";
                return RedirectToAction("Index", "Productos");
            }

            if (cantidad < 1)
            {
                cantidad = 1;
            }

            var carrito = ObtenerCarrito();

            var itemExistente = carrito.Items.FirstOrDefault(x => x.IdProducto == idProducto);

            if (itemExistente == null)
            {
                if (cantidad > producto.Stock)
                {
                    TempData["Error"] = "No hay suficiente stock disponible.";
                    return RedirectToAction("Index", "Productos");
                }

                carrito.Items.Add(new CarritoItem
                {
                    IdProducto = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = cantidad,
                    StockDisponible = producto.Stock
                });
            }
            else
            {
                if ((itemExistente.Cantidad + cantidad) > producto.Stock)
                {
                    TempData["Error"] = "No hay suficiente stock disponible.";
                    return RedirectToAction("Index");
                }

                itemExistente.Cantidad += cantidad;
                itemExistente.StockDisponible = producto.Stock;
            }

            GuardarCarrito(carrito);

            TempData["Mensaje"] = "Producto agregado al carrito.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ActualizarCantidad(int idProducto, int cantidad)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.Items.FirstOrDefault(x => x.IdProducto == idProducto);

            if (item == null)
            {
                TempData["Error"] = "Producto no encontrado en el carrito.";
                return RedirectToAction("Index");
            }

            var producto = db.Producto.FirstOrDefault(p => p.Id == idProducto && p.Activo);
            if (producto == null)
            {
                TempData["Error"] = "Producto no disponible.";
                return RedirectToAction("Index");
            }

            if (cantidad <= 0)
            {
                carrito.Items.Remove(item);
            }
            else
            {
                if (cantidad > producto.Stock)
                {
                    TempData["Error"] = "La cantidad supera el stock disponible.";
                    return RedirectToAction("Index");
                }

                item.Cantidad = cantidad;
                item.StockDisponible = producto.Stock;
                item.Precio = producto.Precio;
            }

            GuardarCarrito(carrito);

            TempData["Mensaje"] = "Carrito actualizado.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int idProducto)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.Items.FirstOrDefault(x => x.IdProducto == idProducto);

            if (item != null)
            {
                carrito.Items.Remove(item);
                GuardarCarrito(carrito);
            }

            TempData["Mensaje"] = "Producto eliminado del carrito.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Vaciar()
        {
            Session[CarritoSessionKey] = new Carrito();
            TempData["Mensaje"] = "Carrito vaciado.";
            return RedirectToAction("Index");
        }
    }
}