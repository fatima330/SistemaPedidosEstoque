using SistemaPedidosEstoque.DTOs.Produto;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoResponse>> ObterTodosAsync()
    {
        var produtos = await _produtoRepository.ObterTodosAsync();
        return produtos.Select(p => new ProdutoResponse
        {
            Id = p.Id,
            FornecedorId = p.FornecedorId,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Tipo = p.Tipo,
            QtdeEstoque = p.QtdeEstoque,
            Ativo = p.Ativo,
            DataCadastro = p.DataCadastro
        });
    }

    public async Task<ProdutoResponse?> ObterPorIdAsync(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto is null)
            return null;

        return new ProdutoResponse
        {
            Id = produto.Id,
            FornecedorId = produto.FornecedorId,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Tipo = produto.Tipo,
            QtdeEstoque = produto.QtdeEstoque,
            Ativo = produto.Ativo,
            DataCadastro = produto.DataCadastro
        };
    }

    public async Task<int> AdicionarAsync(ProdutoRequest request)
    {
        var produto = new Produto
        {
            FornecedorId = request.FornecedorId,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Tipo = request.Tipo,
            QtdeEstoque = request.QtdeEstoque,
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        return await _produtoRepository.AdicionarAsync(produto);
    }

    public async Task AtualizarAsync(int id, ProdutoRequest request)
    {
        var produto = new Produto
        {
            Id = id,
            FornecedorId = request.FornecedorId,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Tipo = request.Tipo,
            QtdeEstoque = request.QtdeEstoque,
            Ativo = true
        };

        await _produtoRepository.AtualizarAsync(produto);
    }

    public async Task RemoverAsync(int id)
    {
        await _produtoRepository.RemoverAsync(id);
    }
}
