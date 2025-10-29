using ProvaAPI.Exceptions;
using System.Data;
using System.Linq.Expressions;

namespace ProvaAPI.Models
{
    public class ProdutoModel
    {

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; } = string.Empty;
        public decimal Preco {  get; private set; }                   
        public int Quantidade { get; private set; }

        public ProdutoModel(Guid Id, string Nome, decimal Preco, int Quantidade)
        {

            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("O nome do produto é obrigatório");
            
            if (Preco <= 0)
                throw new DomainException("O preço do produto deve ser maior que zero ");
            
            Id = Guid.NewGuid();
            Nome = Nome.Trim();
            Preco = Preco;
        }

        //Construtor privado, para uso do Entity Framework
        public ProdutoModel() { }
    }
}
