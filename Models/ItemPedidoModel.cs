namespace ProvaAPI.Models
{
    public class ItemPedidoModel
    {

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public decimal PrecoUnitario{ get; private set; }

        //Criando o construtor para receber os dados do produto
        //Ao criar um ItemPedido será passado um produto como base
       public ItemPedidoModel (ProdutoModel produto)
        {
            Id = Guid.NewGuid();
            ProdutoId = produto.Id;
            NomeProduto = produto.Nome;
            PrecoUnitario = produto.Preco;
        }

        private ItemPedidoModel() { }

    }
}
