using Microsoft.EntityFrameworkCore;
using ProvaAPI.Data;
using ProvaAPI.Models;

namespace ProvaAPI.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {

        //Injetando a dependência
        private readonly ProvaApiDbContext _context;

        public PedidoRepository(ProvaApiDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoModel?> ListarPorIdAsync (Guid id)
        {
            return await _context.Pedidos
                                                    .Include(p => p.Itens) 
                                                    .FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task <IEnumerable<PedidoModel>> ListarTodosAsync (PedidoStatus? status = null, int pagina = 1, int tamanho = 10)
        {
            var query = _context.Pedidos.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status);
            }

            return await query
                .Skip((pagina - 1)* tamanho)
                .Take(tamanho
                ).ToListAsync();
        }

        public async Task AdicionarAsync (PedidoModel pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync (PedidoModel pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }



    }
}
