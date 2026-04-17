namespace tiendaDeportivaAPI.Models.Dto
{
    public class DetallePedidoDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public ProductoDto Producto { get; set; }
    }
}