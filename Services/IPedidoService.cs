using ProvaAPI.Models;

namespace ProvaAPI.Services
{
    public interface IPedidoService
    {

        Task<PedidoModel> IniciarNovoPedidoAsync();
        Task AdicionarProdutoAoPedidoAsync(Guid id,  string nomeProduto, decimal preco);
        Task RemoverItemDoPedidoAsync(Guid pedidoId, Guid itemId);
        Task FecharPedidoAsync(Guid pedidoId);
        Task<PedidoModel> ListarPedidoPorIdAsync (Guid id);
        Task<IEnumerable<PedidoModel>> ListarTodosPedidosAsync(PedidoStatus? Status = null);

    }
}
