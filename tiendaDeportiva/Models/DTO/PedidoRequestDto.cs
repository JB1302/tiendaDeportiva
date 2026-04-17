using System.Collections.Generic;

namespace tiendaDeportiva.Models.Dto
{
    public class PedidoRequestDto
    {
        public List<PedidoItemRequestDto> Items { get; set; } = new List<PedidoItemRequestDto>();
    }
}