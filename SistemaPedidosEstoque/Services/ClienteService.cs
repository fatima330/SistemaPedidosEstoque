using SistemaPedidosEstoque.DTOs.Cliente;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Services;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteResponse>> ObterTodosAsync()
    {
        var clientes = await _clienteRepository.ObterTodosAsync();
        return clientes.Select(c => new ClienteResponse
        {
            Id = c.Id,
            Cpf = c.Cpf,
            Nome = c.Nome,
            Endereco = c.Endereco,
            Email = c.Email,
            DataCadastro = c.DataCadastro,
            Telefone = c.Telefone
        });
    }

    public async Task<ClienteResponse?> ObterPorIdAsync(int id)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id);
        if (cliente is null)
            return null;

        return new ClienteResponse
        {
            Id = cliente.Id,
            Cpf = cliente.Cpf,
            Nome = cliente.Nome,
            Endereco = cliente.Endereco,
            Email = cliente.Email,
            DataCadastro = cliente.DataCadastro,
            Telefone = cliente.Telefone
        };
    }

    public async Task<int> AdicionarAsync(ClienteRequest request)
    {
        var cliente = new Cliente
        {
            Cpf = request.Cpf,
            Nome = request.Nome,
            Endereco = request.Endereco,
            Email = request.Email,
            DataCadastro = DateTime.Now,
            Telefone = request.Telefone
        };

        return await _clienteRepository.AdicionarAsync(cliente);
    }

    public async Task AtualizarAsync(int id, ClienteRequest request)
    {
        var cliente = new Cliente
        {
            Id = id,
            Cpf = request.Cpf,
            Nome = request.Nome,
            Endereco = request.Endereco,
            Email = request.Email,
            Telefone = request.Telefone
        };

        await _clienteRepository.AtualizarAsync(cliente);
    }

    public async Task RemoverAsync(int id)
    {
        await _clienteRepository.RemoverAsync(id);
    }
}
