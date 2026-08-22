using Dapper;
using Oracle.ManagedDataAccess.Client;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        const string sql = "SELECT ID_CLIENTE AS Id, CPF AS Cpf, NOME AS Nome, ENDERECO AS Endereco, EMAIL AS Email, DATA_CADASTRO AS DataCadastro, TELEFONE AS Telefone FROM TB_CLIENTES_SISTEMA";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryAsync<Cliente>(sql);
    }

    public async Task<Cliente?> ObterPorIdAsync(int id)
    {
        const string sql = @"SELECT ID_CLIENTE AS Id, CPF AS Cpf, NOME AS Nome, ENDERECO AS Endereco, 
                             EMAIL AS Email, DATA_CADASTRO AS DataCadastro, TELEFONE AS Telefone 
                             FROM TB_CLIENTES_SISTEMA WHERE ID_CLIENTE = :Id";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
    }

    public async Task<int> AdicionarAsync(Cliente cliente)
    {
        const string sql = @"INSERT INTO TB_CLIENTES_SISTEMA (ID_CLIENTE, CPF, NOME, ENDERECO, EMAIL, DATA_CADASTRO, TELEFONE)
                             VALUES (TB_CLIENTES_SISTEMA_SEQ.NEXTVAL, :Cpf, :Nome, :Endereco, :Email, :DataCadastro, :Telefone)
                             RETURNING ID_CLIENTE INTO :Id";

        using var connection = new OracleConnection(_connectionString);
        var parametros = new DynamicParameters();
        parametros.Add("Cpf", cliente.Cpf);
        parametros.Add("Nome", cliente.Nome);
        parametros.Add("Endereco", cliente.Endereco);
        parametros.Add("Email", cliente.Email);
        parametros.Add("DataCadastro", cliente.DataCadastro);
        parametros.Add("Telefone", cliente.Telefone);
        parametros.Add("Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await connection.ExecuteAsync(sql, parametros);
        return parametros.Get<int>("Id");
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        const string sql = @"UPDATE TB_CLIENTES_SISTEMA 
                             SET CPF = :Cpf, NOME = :Nome, ENDERECO = :Endereco, EMAIL = :Email, TELEFONE = :Telefone
                             WHERE ID_CLIENTE = :Id";

        using var connection = new OracleConnection(_connectionString);
        await connection.ExecuteAsync(sql, cliente);
    }

    public async Task RemoverAsync(int id)
    {
        const string sql = "DELETE FROM TB_CLIENTES_SISTEMA WHERE ID_CLIENTE = :Id";

        using var connection = new OracleConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
