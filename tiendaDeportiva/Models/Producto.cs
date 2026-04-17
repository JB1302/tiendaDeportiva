using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tiendaDeportiva.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        public decimal Precio { get; set; } = decimal.Zero;



    }
}