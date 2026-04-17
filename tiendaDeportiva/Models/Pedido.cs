using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using tiendaDeportiva.Models.Enum;

namespace tiendaDeportiva.Models
{
    public class Pedido
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha del Pedido")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "ID del Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;

        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        // Pedido
        public virtual ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
    }
}