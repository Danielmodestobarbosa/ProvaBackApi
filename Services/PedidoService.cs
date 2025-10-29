using ProvaAPI.Exceptions;
using ProvaAPI.Models;
using ProvaAPI.Repositories;

namespace ProvaAPI.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoModel> IniciarNovoPedidoAsync()
        {
            var pedido = new PedidoModel();
            await _pedidoRepository.AdicionarAsync(pedido);
            return pedido;
        }

        public async Task AdicionarProdutoAoPedidoAsync (Guid pedidoId, string nomeProduto, decimal preco)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync (pedidoId)
                ?? throw new DomainException("O pedido não foi encontrado");

            var produto = new ProdutoModel(Guid.NewGuid(), nomeProduto, preco, 1);
            var item = new ItemPedidoModel(produto);

            pedido.AdicionarItem(item);
            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task RemoverItemDoPedidoAstync (Guid pedidoId, Guid itemId)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId)
                ?? throw new DomainException("O pedido não foi encotrado");

            pedido.RemoverItem(itemId);
            await _pedidoRepository.AtualizarAsync (pedido);
        }

        public async Task FecharPedidoAsync (Guid pedidoId)
        {

            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId)
                ?? throw new DomainException("O pedido não foi encontrado");

            pedido.FecharPedido();
            await _pedidoRepository.AtualizarAsync(pedido); 
        }

        public async Task<PedidoModel> ListarPedidoPorIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(id)
                ?? throw new DomainException("O pedido não foi encontrado");

            return pedido;
        }

        public async Task<IEnumerable<PedidoModel>> ListarTodosPedidosAsync (PedidoStatus? Status = null, int pagina = 1, int tamanho = 10)
        {
            if (pagina < 1) pagina = 1;
            if(tamanho < 1) tamanho = 10,
            if (tamanho > 100) tamanho = 100;

            return await _pedidoRepository.ListarTodosAsync(Status, pagina, tamanho);
        }
}
