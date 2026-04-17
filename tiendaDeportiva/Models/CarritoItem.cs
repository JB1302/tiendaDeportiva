using System.ComponentModel.DataAnnotations;

namespace tiendaDeportiva.Models
{
    public class CarritoItem
    {
        public int IdProducto { get; set; }

        [Display(Name = "Producto")]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        public int StockDisponible { get; set; }

        public decimal Subtotal
        {
            get { return Precio * Cantidad; }
        }
    }
}