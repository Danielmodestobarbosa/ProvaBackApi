using ProvaAPI.Models;

namespace ProvaAPI.DTOs
{
    public class PedidoResponse
    {

        public Guid Id { get; set; }
        public DateTime CriadoEm {  get; set; }
        public DateTime? FechadoEm { get; set; }
        public List<ItemPedidoDto> Itens { get; set; } = new();
        public string  Status { get; set; }

        public PedidoResponse ToResponse(PedidoModel pedido) 
        {
            return new PedidoResponse
            {
                Id = pedido.Id,
                Status = pedido.Status.ToString(),
                CriadoEm = pedido.CriadoEm,

            };

        }

    }
}
