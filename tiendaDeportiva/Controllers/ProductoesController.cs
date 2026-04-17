using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using tiendaDeportiva.Models;

namespace tiendaDeportiva.Controllers
{
    public class ProductoesController : Controller
    {
        private HttpClient CrearCliente()
        {
            var apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            var client = new HttpClient();
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private async Task<string> LeerError(HttpResponseMessage response)
        {
            var contenido = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contenido))
                return "Ocurrió un error al consumir el API.";

            return contenido;
        }

        // GET: Productoes
        public async Task<ActionResult> Index()
        {
            using (var client = CrearCliente())
            {
                // Usa "api/productos/activos" si quieres mostrar solo los activos
                var response = await client.GetAsync("api/productos");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return View(new List<Producto>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json) ?? new List<Producto>();

                return View(productos);
            }
        }

        // GET: Productoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var client = CrearCliente())
            {
                var response = await client.GetAsync("api/productos/" + id.Value);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return HttpNotFound();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<Producto>(json);

                if (producto == null)
                    return HttpNotFound();

                return View(producto);
            }
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Precio,Stock,Categoria,Activo")] Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            using (var client = CrearCliente())
            {
                var payload = new
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    Categoria = Convert.ToInt32(producto.Categoria)
                };

                var jsonRequest = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/productos", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return View(producto);
                }

                TempData["Mensaje"] = "Producto creado correctamente.";
                return RedirectToAction("Index");
            }
        }

        // GET: Productoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var client = CrearCliente())
            {
                var response = await client.GetAsync("api/productos/" + id.Value);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return HttpNotFound();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<Producto>(json);

                if (producto == null)
                    return HttpNotFound();

                return View(producto);
            }
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Precio,Stock,Categoria,Activo")] Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            using (var client = CrearCliente())
            {
                var payload = new
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    Categoria = Convert.ToInt32(producto.Categoria)
                };

                var jsonRequest = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("api/productos/" + producto.Id, content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return View(producto);
                }

                TempData["Mensaje"] = "Producto actualizado correctamente.";
                return RedirectToAction("Index");
            }
        }

        // GET: Productoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var client = CrearCliente())
            {
                var response = await client.GetAsync("api/productos/" + id.Value);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return HttpNotFound();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var producto = JsonConvert.DeserializeObject<Producto>(json);

                if (producto == null)
                    return HttpNotFound();

                return View(producto);
            }
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = CrearCliente())
            {
                var response = await client.DeleteAsync("api/productos/" + id);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Delete", new { id = id });
                }

                TempData["Mensaje"] = "Producto eliminado correctamente.";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}