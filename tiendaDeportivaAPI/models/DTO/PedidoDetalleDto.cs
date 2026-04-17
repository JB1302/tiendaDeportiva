using System;
using System.Collections.Generic;
using tiendaDeportiva.Models.Enum;

namespace tiendaDeportivaAPI.Models.Dto
{
    public class PedidoDetalleDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdUsuario { get; set; }
        public decimal MontoTotal { get; set; }
        public EstadoPedido Estado { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
    }
}