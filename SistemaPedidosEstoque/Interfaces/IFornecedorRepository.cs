using SistemaPedidosEstoque.Entities;

namespace SistemaPedidosEstoque.Interfaces;

public interface IFornecedorRepository
{
    Task<IEnumerable<Fornecedor>> ObterTodosAsync();
    Task<Fornecedor?> ObterPorIdAsync(int id);
    Task<int> AdicionarAsync(Fornecedor fornecedor);
    Task AtualizarAsync(Fornecedor fornecedor);
    Task RemoverAsync(int id);
}
