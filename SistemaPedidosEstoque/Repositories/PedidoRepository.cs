using Dapper;
using Oracle.ManagedDataAccess.Client;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
        }

        public async Task<int> AdicionarAsync(Pedido pedido)
        {
            const string sql = @"INSERT INTO PEDIDO 
                                (ID_PEDIDO, ID_CLIENTE, ID_PRODUTO, QUANTIDADE) 
                                VALUES (TB_PEDIDO_SEQ.NEXTVAL, :IdCliente, :IdProduto, :Quantidade) 
                                RETURNING ID_PEDIDO INTO :Id";

            using var connection = new OracleConnection(_connectionString);

            var parameters = new DynamicParameters();

            parameters.Add("IdCliente", pedido.IdCliente);
            parameters.Add("IdProduto", pedido.IdProduto);
            parameters.Add("Quantidade", pedido.Quantidade);
            parameters.Add("Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters);

            return parameters.Get<int>("Id");
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            const string sql = @"UPDATE PEDIDO 
                                SET ID_CLIENTE = :IdCliente,
                                ID_PRODUTO = :IdProduto, 
                                QUANTIDADE = :Quantidade WHERE ID_PEDIDO = :IdPedido";

            using var connection = new OracleConnection(_connectionString);
            var parameters = new DynamicParameters(pedido);
            parameters.Add("IdPedido", pedido.IdPedido);
            parameters.Add("IdCliente", pedido.IdCliente);
            parameters.Add("IdProduto", pedido.IdProduto);
            parameters.Add("Quantidade", pedido.Quantidade);

            await connection.ExecuteAsync(sql, parameters);

        }

        public async Task<Pedido?> ObterPorIdAsync(int id)
        {
             const string sql = @"SELECT ID_PEDIDO AS IdPedido,
                                     ID_CLIENTE AS IdCliente,
                                     ID_PRODUTO AS IdProduto,
                                     QUANTIDADE AS Quantidade
                              FROM PEDIDO
                              WHERE ID_PEDIDO = :Id"; 

            using var connection = new OracleConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Pedido>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            const string sql = @"SELECT ID_PEDIDO AS IdPedido,
                                     ID_CLIENTE AS IdCliente,
                                     ID_PRODUTO AS IdProduto,
                                     QUANTIDADE AS Quantidade
                              FROM PEDIDO";

            using var connection = new OracleConnection(_connectionString);
            return await connection.QueryAsync<Pedido>(sql);
        }

        public async Task RemoverAsync(int id)
        {
            const string sql = @"DELETE FROM PEDIDO WHERE ID_PEDIDO = :Id";

            using var connection = new OracleConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
