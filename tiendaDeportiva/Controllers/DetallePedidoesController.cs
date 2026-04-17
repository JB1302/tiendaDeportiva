using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tiendaDeportiva.Models;

namespace tiendaDeportiva.Controllers
{
    public class DetallePedidoesController : Controller
    {
        private DBContextController db = new DBContextController();

        // GET: DetallePedidoes
        public ActionResult Index()
        {
            var detallePedido = db.DetallePedido.Include(d => d.Pedido).Include(d => d.Producto);
            return View(detallePedido.ToList());
        }

        // GET: DetallePedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        public ActionResult Create()
        {
            ViewBag.IdPedido = new SelectList(db.Pedido, "Id", "IdUsuario");
            ViewBag.IdProducto = new SelectList(db.Producto, "Id", "Nombre");
            return View();
        }

        // POST: DetallePedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.DetallePedido.Add(detallePedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPedido = new SelectList(db.Pedido, "Id", "IdUsuario", detallePedido.IdPedido);
            ViewBag.IdProducto = new SelectList(db.Producto, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPedido = new SelectList(db.Pedido, "Id", "IdUsuario", detallePedido.IdPedido);
            ViewBag.IdProducto = new SelectList(db.Producto, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPedido = new SelectList(db.Pedido, "Id", "IdUsuario", detallePedido.IdPedido);
            ViewBag.IdProducto = new SelectList(db.Producto, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            db.DetallePedido.Remove(detallePedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
