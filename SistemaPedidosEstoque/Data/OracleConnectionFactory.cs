using Oracle.ManagedDataAccess.Client;

namespace SistemaPedidosEstoque.Data
{
    public class OracleConnectionFactory
    {
        private readonly string _connectionString;

        public OracleConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "A connection string 'DefaultConnection' não foi encontrada.");
        }

        public OracleConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
