namespace ProvaAPI.DTOs
{
    public class ItemPedidoDto
    {

        public Guid Id {  get; set; }
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }

    }
}
