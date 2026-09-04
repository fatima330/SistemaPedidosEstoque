using Dapper;
using Oracle.ManagedDataAccess.Client;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;
using System.Data;

namespace SistemaPedidosEstoque.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly string _connectionString;

    public ProdutoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        const string sql = @"SELECT ID_PRODUTO AS Id,
                                     ID_FORNECEDOR AS FornecedorId,
                                     NOME,
                                     DESCRICAO,
                                     TIPO,
                                     QTDE_ESTOQUE AS QtdeEstoque,
                                     ATIVO AS Ativo,
                                     DATA_CADASTRO AS DataCadastro
                              FROM TB_PRODUTO_SISTEMA";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryAsync<Produto>(sql);
    }

    public async Task<Produto?> ObterPorIdAsync(int id)
    {
        const string sql = @"SELECT ID_PRODUTO AS Id,
                                     ID_FORNECEDOR AS FornecedorId,
                                     NOME,
                                     DESCRICAO,
                                     TIPO,
                                     QTDE_ESTOQUE AS QtdeEstoque,
                                     ATIVO AS Ativo,
                                     DATA_CADASTRO AS DataCadastro
                              FROM TB_PRODUTO_SISTEMA
                              WHERE ID_PRODUTO = :Id";

        using var connection = new OracleConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
    }

    public async Task<int> AdicionarAsync(Produto produto)
    {
        const string sql = @"INSERT INTO TB_PRODUTO_SISTEMA (ID_PRODUTO, ID_FORNECEDOR, NOME, DESCRICAO, TIPO, QTDE_ESTOQUE, ATIVO, DATA_CADASTRO)
                              VALUES (TB_PRODUTO_SISTEMA_SEQ.NEXTVAL, :FornecedorId, :Nome, :Descricao, :Tipo, :QtdeEstoque, :Ativo, :DataCadastro)
                              RETURNING ID_PRODUTO INTO :Id";

        using var connection = new OracleConnection(_connectionString);
        var parametros = new DynamicParameters();
        parametros.Add("FornecedorId", produto.FornecedorId);
        parametros.Add("Nome", produto.Nome);
        parametros.Add("Descricao", produto.Descricao);
        parametros.Add("Tipo", produto.Tipo);
        parametros.Add("QtdeEstoque", produto.QtdeEstoque);
        parametros.Add("Ativo", produto.Ativo ? 1 : 0);
        parametros.Add("DataCadastro", produto.DataCadastro);
        parametros.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await connection.ExecuteAsync(sql, parametros);
        return parametros.Get<int>("Id");
    }

    public async Task AtualizarAsync(Produto produto)
    {
        const string sql = @"UPDATE TB_PRODUTO_SISTEMA
                              SET ID_FORNECEDOR = :FornecedorId,
                                  NOME = :Nome,
                                  DESCRICAO = :Descricao,
                                  TIPO = :Tipo,
                                  QTDE_ESTOQUE = :QtdeEstoque,
                                  ATIVO = :Ativo
                              WHERE ID_PRODUTO = :Id";

        using var connection = new OracleConnection(_connectionString);
        var parametros = new DynamicParameters();
        parametros.Add("FornecedorId", produto.FornecedorId);
        parametros.Add("Nome", produto.Nome);
        parametros.Add("Descricao", produto.Descricao);
        parametros.Add("Tipo", produto.Tipo);
        parametros.Add("QtdeEstoque", produto.QtdeEstoque);
        parametros.Add("Ativo", produto.Ativo ? 1 : 0);
        parametros.Add("Id", produto.Id);

        await connection.ExecuteAsync(sql, parametros);
    }

    public async Task RemoverAsync(int id)
    {
        const string sql = "DELETE FROM TB_PRODUTO_SISTEMA WHERE ID_PRODUTO = :Id";

        using var connection = new OracleConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
