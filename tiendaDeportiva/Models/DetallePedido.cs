using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using tiendaDeportiva.Helpers;

namespace tiendaDeportiva.Models
{
    public class DetallePedido
    {
        [Key]
        [Display(Name = "ID del Detalle del Pedido")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pedido")]
        [Display(Name = "ID del Pedido")]
        public int IdPedido { get; set; }


        [Required]
        [ForeignKey("Producto")]
        [Display(Name = "ID del Producto")]
        public int IdProducto { get; set; }

        [MayorOIgualQueCero]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [MayorOIgualQueCero]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}