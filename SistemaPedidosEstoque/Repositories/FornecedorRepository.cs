using Dapper;
using Oracle.ManagedDataAccess.Client;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;
using System.Data;

namespace SistemaPedidosEstoque.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly string _connectionString;

    public FornecedorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
    }

    public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
    {
        const string sql = @"SELECT ID_FORNECEDOR AS Id,
                                     NOME,
                                     CNPJ,
                                     EMAIL,
                                     TELEFONE,
                                     ATIVO AS Ativo
                              FROM TB_FORNECEDOR";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryAsync<Fornecedor>(sql);
    }

    public async Task<Fornecedor?> ObterPorIdAsync(int id)
    {
        const string sql = @"SELECT ID_FORNECEDOR AS Id,
                                     NOME,
                                     CNPJ,
                                     EMAIL,
                                     TELEFONE,
                                     ATIVO AS Ativo
                              FROM TB_FORNECEDOR
                              WHERE ID_FORNECEDOR = :Id";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Fornecedor>(sql, new { Id = id });
    }

    public async Task<int> AdicionarAsync(Fornecedor fornecedor)
    {
        const string sql = @"INSERT INTO TB_FORNECEDOR (ID_FORNECEDOR, NOME, CNPJ, EMAIL, TELEFONE, ATIVO)
                              VALUES (TB_FORNECEDOR_SEQ.NEXTVAL, :Nome, :Cnpj, :Email, :Telefone, :Ativo)
                              RETURNING ID_FORNECEDOR INTO :Id";

        using var connection = new OracleConnection(_connectionString);
        var parametros = new DynamicParameters();
        parametros.Add("Nome", fornecedor.Nome);
        parametros.Add("Cnpj", fornecedor.Cnpj);
        parametros.Add("Email", fornecedor.Email);
        parametros.Add("Telefone", fornecedor.Telefone);
        parametros.Add("Ativo", fornecedor.Ativo ? 1 : 0);
        parametros.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await connection.ExecuteAsync(sql, parametros);
        return parametros.Get<int>("Id");
    }

    public async Task AtualizarAsync(Fornecedor fornecedor)
    {
        const string sql = @"UPDATE TB_FORNECEDOR
                              SET NOME = :Nome,
                                  CNPJ = :Cnpj,
                                  EMAIL = :Email,
                                  TELEFONE = :Telefone,
                                  ATIVO = :Ativo
                              WHERE ID_FORNECEDOR = :Id";

        using var connection = new OracleConnection(_connectionString);
        var parametros = new DynamicParameters();
        parametros.Add("Nome", fornecedor.Nome);
        parametros.Add("Cnpj", fornecedor.Cnpj);
        parametros.Add("Email", fornecedor.Email);
        parametros.Add("Telefone", fornecedor.Telefone);
        parametros.Add("Ativo", fornecedor.Ativo ? 1 : 0);
        parametros.Add("Id", fornecedor.Id);

        await connection.ExecuteAsync(sql, parametros);
    }

    public async Task RemoverAsync(int id)
    {
        const string sql = "DELETE FROM TB_FORNECEDOR WHERE ID_FORNECEDOR = :Id";

        using var connection = new OracleConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
