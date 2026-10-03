using SistemaPedidosEstoque.Entities;

namespace SistemaPedidosEstoque.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task<Pedido?> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(int id);
    }
}
