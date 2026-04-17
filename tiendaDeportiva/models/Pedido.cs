using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tiendaDeportiva.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [Required]
        public string IdUsuario { get; set; }

        public decimal MontoTotal { get; set; }

        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;

        public List<DetallePedido> Detalles { get; set; } = new();
    }
}