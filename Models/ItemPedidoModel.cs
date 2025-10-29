namespace ProvaAPI.Models
{
    public class ItemPedidoModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public ItemPedidoModel Pedido { get; set; }
        public ProdutoModel Produto { get; set; }


    }
}
