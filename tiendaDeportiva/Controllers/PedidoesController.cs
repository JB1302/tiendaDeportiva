using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using tiendaDeportiva.Models;
using tiendaDeportiva.Models.Dto;

namespace tiendaDeportiva.Controllers
{
    public class PedidoesController : Controller
    {
        private const string CarritoSessionKey = "CARRITO";

        private HttpClient CrearCliente()
        {
            var apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            var client = new HttpClient();
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var token = Session["Token"] as string;

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        private Carrito ObtenerCarrito()
        {
            if (Session[CarritoSessionKey] == null)
            {
                Session[CarritoSessionKey] = new Carrito();
            }

            return (Carrito)Session[CarritoSessionKey];
        }

        private async Task<string> LeerError(HttpResponseMessage response)
        {
            var contenido = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contenido))
            {
                return "Ocurrió un error al consumir el API.";
            }

            try
            {
                var apiError = JsonConvert.DeserializeObject<ApiErrorDto>(contenido);

                if (apiError != null && !string.IsNullOrWhiteSpace(apiError.error))
                {
                    return apiError.error;
                }
            }
            catch
            {
            }

            return contenido;
        }

        // GET: Pedidoes
        public async Task<ActionResult> Index()
        {
            using (var client = CrearCliente())
            {
                var endpoint = User.IsInRole("Admin")
                    ? "api/pedidos"
                    : "api/pedidos/mios";

                var response = await client.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return View(new List<Pedido>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();

                return View(pedidos);
            }
        }

        // GET: Pedidoes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var client = CrearCliente())
            {
                var response = await client.GetAsync("api/pedidos/" + id);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return HttpNotFound();
                }

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var pedido = JsonConvert.DeserializeObject<Pedido>(json);

                if (pedido == null)
                {
                    return HttpNotFound();
                }

                return View(pedido);
            }
        }

        // POST: Pedidoes/CrearDesdeCarrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CrearDesdeCarrito()
        {
            var carrito = ObtenerCarrito();

            if (carrito == null || carrito.Items == null || !carrito.Items.Any())
            {
                TempData["Error"] = "El carrito está vacío.";
                return RedirectToAction("Index", "Carrito");
            }

            var request = new PedidoRequestDto
            {
                Items = carrito.Items.Select(x => new PedidoItemRequestDto
                {
                    ProductoId = x.IdProducto,
                    Cantidad = x.Cantidad
                }).ToList()
            };

            using (var client = CrearCliente())
            {
                var jsonRequest = JsonConvert.SerializeObject(request);

                var content = new StringContent(
                    jsonRequest,
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("api/pedidos", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = await LeerError(response);
                    return RedirectToAction("Index", "Carrito");
                }

                Session[CarritoSessionKey] = new Carrito();
                TempData["Mensaje"] = "Pedido creado correctamente.";

                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
