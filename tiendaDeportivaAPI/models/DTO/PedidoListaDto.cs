using System;
using tiendaDeportiva.Models.Enum;

namespace tiendaDeportivaAPI.Models.Dto
{
    public class PedidoListaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdUsuario { get; set; }
        public decimal MontoTotal { get; set; }
        public EstadoPedido Estado { get; set; }
    }
}