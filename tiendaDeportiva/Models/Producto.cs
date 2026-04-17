using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using tiendaDeportiva.Helpers;
using tiendaDeportiva.Models.Enum;

namespace tiendaDeportiva.Models
{
    public class Producto
    {
        [Key]
        [Display(Name = "ID del Producto")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; } = null;

        [StringLength(500)]
        [Display(Name = "Descripción del Producto")]
        public string Descripcion { get; set; } = null;

        [MayorOIgualQueCero]
        [Display(Name = "Precio del Producto")]
        public decimal Precio { get; set; } = 0;

        [MayorOIgualQueCero]
        [Display(Name = "Stock del Producto")]
        public int Stock { get; set; } = 0;

        [Required]
        [Display(Name = "Categoría del Producto")]
        public Categoria Categoria { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool Activo { get; set; } = true;

        public virtual ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
    }

}