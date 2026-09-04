using SistemaPedidosEstoque.DTOs.Fornecedor;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Services;

public class FornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorService(IFornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<IEnumerable<FornecedorResponse>> ObterTodosAsync()
    {
        var fornecedores = await _fornecedorRepository.ObterTodosAsync();
        return fornecedores.Select(f => new FornecedorResponse
        {
            Id = f.Id,
            Nome = f.Nome,
            Cnpj = f.Cnpj,
            Email = f.Email,
            Telefone = f.Telefone,
            Ativo = f.Ativo
        });
    }

    public async Task<FornecedorResponse?> ObterPorIdAsync(int id)
    {
        var fornecedor = await _fornecedorRepository.ObterPorIdAsync(id);
        if (fornecedor is null)
            return null;

        return new FornecedorResponse
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Cnpj = fornecedor.Cnpj,
            Email = fornecedor.Email,
            Telefone = fornecedor.Telefone,
            Ativo = fornecedor.Ativo
        };
    }

    public async Task<int> AdicionarAsync(FornecedorRequest request)
    {
        var fornecedor = new Fornecedor
        {
            Nome = request.Nome,
            Cnpj = request.Cnpj,
            Email = request.Email,
            Telefone = request.Telefone,
            Ativo = true
        };

        return await _fornecedorRepository.AdicionarAsync(fornecedor);
    }

    public async Task AtualizarAsync(int id, FornecedorRequest request)
    {
        var fornecedor = new Fornecedor
        {
            Id = id,
            Nome = request.Nome,
            Cnpj = request.Cnpj,
            Email = request.Email,
            Telefone = request.Telefone,
            Ativo = true
        };

        await _fornecedorRepository.AtualizarAsync(fornecedor);
    }

    public async Task RemoverAsync(int id)
    {
        await _fornecedorRepository.RemoverAsync(id);
    }
}
