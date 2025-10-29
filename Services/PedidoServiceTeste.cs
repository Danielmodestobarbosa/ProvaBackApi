using ProvaAPI.Exceptions;
using ProvaAPI.Repositories;
using Moq;
using Xunit;
using ProvaAPI.Models;

namespace ProvaAPI.Services
{
    public class PedidoServiceTeste
    {
        [Fact]
        public async Task IniciarNovoPedido_DeveCriarPedidoComStatusAberto()
        {
            // Arrange
            var mockRepo = new Mock<IPedidoRepository>();
            var service = new PedidoService(mockRepo.Object);

            // Act
            var pedido = await service.IniciarNovoPedidoAsync();

            // Assert
            Assert.False(pedido.Status == PedidoStatus.Fechado);
            Assert.Empty(pedido.Itens);
        }

        [Fact]
        public async Task FecharPedido_ComItens_DeveFecharComSucesso()
        {
            // Arrange
            var mockRepo = new Mock<IPedidoRepository>();
            var service = new PedidoService(mockRepo.Object);
            var pedido = new PedidoModel();
            var produto = new ProdutoModel(Guid.NewGuid(), "Notebook", 2500m, 5);
            var item = new ItemPedidoModel(produto);
            pedido.AdicionarItem(item);

            mockRepo.Setup(r => r.ListarPorIdAsync(pedido.Id)).ReturnsAsync(pedido);
            mockRepo.Setup(r => r.AtualizarAsync(It.IsAny<PedidoModel>())).Returns(Task.CompletedTask);

            // Act
            await service.FecharPedidoAsync(pedido.Id);

            // Assert
            Assert.True(pedido.Status == PedidoStatus.Fechado);
        }

        [Fact]
        public async Task FecharPedido_SemItens_DeveLancarExcecao()
        {

            var mockRepo = new Mock<IPedidoRepository>();
            var service = new PedidoService(mockRepo.Object);
            var pedido = new PedidoModel(); // vazio

            mockRepo.Setup(r => r.ListarPorIdAsync(pedido.Id)).ReturnsAsync(pedido);

            await Assert.ThrowsAsync<DomainException>(() => service.FecharPedidoAsync(pedido.Id));
        }

        [Fact]
        public async Task AdicionarProduto_PedidoFechado_DeveLancarExcecao()
        {
            // Arrange
            var mockRepo = new Mock<IPedidoRepository>();
            var service = new PedidoService(mockRepo.Object);
            var pedido = new PedidoModel();
            pedido.FecharPedido(); // força fechar

            mockRepo.Setup(r => r.ListarPorIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() =>
                service.AdicionarProdutoAoPedidoAsync(pedido.Id, "Mouse", 50m));
        }
    }
}
