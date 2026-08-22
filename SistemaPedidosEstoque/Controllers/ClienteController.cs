using Microsoft.AspNetCore.Mvc;
using SistemaPedidosEstoque.DTOs.Cliente;
using SistemaPedidosEstoque.Services;

namespace SistemaPedidosEstoque.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClienteController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> Get()
    {
        var clientes = await _clienteService.ObterTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponse>> Get(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente is null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] ClienteRequest request)
    {
        var id = await _clienteService.AdicionarAsync(request);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ClienteRequest request)
    {
        await _clienteService.AtualizarAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _clienteService.RemoverAsync(id);
        return NoContent();
    }
}
