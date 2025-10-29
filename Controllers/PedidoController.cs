using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using ProvaAPI.DTOs;
using ProvaAPI.Models;
using ProvaAPI.Services;

namespace ProvaAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoService _pedidoService;

        private PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> IniciarNovoPedido()
        {
            var pedido = await _pedidoService.IniciarNovoPedidoAsync();
            return CreatedAtAction(nameof(ListarPedidoPorId), new { id = pedido.Id }, ToResponse(pedido));
        }

        [HttpPost("{id}/itens")]
        public async Task<ActionResult> AdicionarProduto([FromRoute] Guid id, [FromBody] AdicionarProdutoRequest request)
        {
            await _pedidoService.AdicionarProdutoAoPedidoAsync(id, request.Nome, request.Preco);
            return NoContent();
        }

        [HttpDelete("{pedidoId}/itens/{itemId}")]
        public async Task<IActionResult> RemoverItem([FromRoute] Guid pedidoId, [FromRoute], Guid itemId)
        {
            await _pedidoService.RemoverItemDoPedidoAsync(pedidoId, itemId);
            return NoContent();
        }

        [HttpPost("{id}/fechar")]
        public async Task<IActionResult> FecharPedido([FromRoute], Guid id)
        {
            await _pedidoService.FecharPedidoAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> ListarTodosPedidos([FromQuery], PedidoStatus? status = null,
                                                                                                                                                                   [FromQuery], int pagina = 1,
                                                                                                                                                                   [FromQuery], int tamanho = 10)
        {
            var pedidos = await _pedidoService.ListarTodosPedidosAsync();
            return Ok(pedidos.Select(ToResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoResponse>> ListarPedidoPorId (Guid Id)
        {
            var pedido = await _pedidoService.ListarPedidoPorIdAsync(Id);
            return Ok(ToResponse(pedido));
        }

        private static PedidoResponse ToResponse (PedidoModel pedido)
        {
            return new PedidoResponse
            {
                Id = pedido.Id,
                CriadoEm = pedido.CriadoEm,
                FechadoEm = pedido.FechadoEm,
                Itens = pedido.Itens.Select(i => new ItemPedidoDto
                {
                    Id = i.Id,
                    ProdutoId = i.ProdutoId,
                    NomeProduto = i.NomeProduto,
                    PrecoUnitario = i.PrecoUnitario,
                }).ToList(),
            }
        }


    }
}
