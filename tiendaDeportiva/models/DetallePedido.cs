using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tiendaDeportiva.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }

        [Required]
        public int IdPedido { get; set; }

        public Pedido Pedido { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }
        public Producto Producto { get; set; }
    }
}