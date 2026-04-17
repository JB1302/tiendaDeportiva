using System.ComponentModel.DataAnnotations;

namespace tiendaDeportivaAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "nombre obligatorio")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "precio mayor que 0")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "stock mayor o igual a 0")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "categoria obligatoria")]
        public string Categoria { get; set; }

        public bool Activo { get; set; } = true;
    }
}