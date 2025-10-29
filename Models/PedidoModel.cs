using ProvaAPI.Exceptions;

namespace ProvaAPI.Models
{
    public class PedidoModel
    {

        public Guid Id {  get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime FechadoEm { get; private set; }
        public PedidoStatus Status { get; private set; }

        private readonly List<ItemPedidoModel> _itens = new();
        public IReadOnlyCollection<ItemPedidoModel> Itens => _itens.AsReadOnly();

        public PedidoModel()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
        }

        public void AdicionarItem (ItemPedidoModel item)
        {
            if(Status == PedidoStatus.Fechado )
                throw new DomainException("Não é possível adicionar itens ao pedido fechado");
            
            if(item == null)
                throw new DomainException("Este item é invalido");
            
            _itens.Add(item);
        }

        public void RemoverItem (Guid itemId)
        {
            if (Status == PedidoStatus.Fechado)
                throw new DomainException("Não é possível remover itens de um pedido fechado"); 

            var item = _itens.FirstOrDefault(i => i.Id == itemId);

            if(item == null) 
                throw new DomainException ("Este item não foi encontrado no pedido")

            _itens.Remove(item);
        }

        public void FecharPedido()
        {
            if (Status == PedidoStatus.Fechado)
                throw new DomainException("Este pedido já está fechado");
            if (!_itens.Any())
                throw new DomainException("Não é possível fechar um pedido sem itens");

            FechadoEm = DateTime.UtcNow;
        }

    }
}
