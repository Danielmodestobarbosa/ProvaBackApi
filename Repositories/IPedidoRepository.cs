using ProvaAPI.Models;

namespace ProvaAPI.Repositories
{

    //Utilizada a interface para isolar e organizar o acesso aos dados
    public interface IPedidoRepository
    {

        Task<PedidoModel?> ListarPorIdAsync(Guid id);
        Task<IEnumerable<PedidoModel>> ListarTodosAsync(PedidoStatus? Status = null, int pagina = 1, int tamanho = 10);
        Task AdicionarAsync (PedidoModel pedido);
        Task AtualizarAsync (PedidoModel pedido);

    }
}
