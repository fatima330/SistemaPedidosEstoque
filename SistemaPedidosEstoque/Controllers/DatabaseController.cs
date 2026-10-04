using Microsoft.AspNetCore.Mvc;
using SistemaPedidosEstoque.Data;

namespace SistemaPedidosEstoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
      private readonly OracleConnectionFactory _connectionFactory;

        public DatabaseController(OracleConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        [HttpGet("testar-conexao")]
        public async Task<IActionResult> TestarConexao()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                await connection.OpenAsync();

                return Ok("Conexão com Oracle realizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar no Oracle: {ex.Message}");
            }
        }
    }
}

