using System.Linq.Expressions;

namespace ProvaAPI.Models
{
    public class ProdutoModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public decimal Preco {  get; set; }

    }
}
