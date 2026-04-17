using System.Collections.Generic;
using System.Linq;

namespace tiendaDeportiva.Models
{
    public class Carrito
    {
        public List<CarritoItem> Items { get; set; } = new List<CarritoItem>();

        public decimal Total
        {
            get { return Items.Sum(x => x.Subtotal); }
        }

        public int CantidadTotal
        {
            get { return Items.Sum(x => x.Cantidad); }
        }
    }
}