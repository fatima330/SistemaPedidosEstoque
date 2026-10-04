using Microsoft.AspNetCore.Mvc;
using SistemaPedidosEstoque.DTOs.Pedido;
using SistemaPedidosEstoque.Services;

namespace SistemaPedidosEstoque.Controllers;
[ApiController]
[Route("api/[controller]")]

public class PedidoController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidoController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _pedidoService.ObterTodosAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);

        if(pedido is null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> PostPedido([FromBody] PedidoRequest request)
    {
        var id = await _pedidoService.AdicionarAsync(request);
        return CreatedAtAction(nameof(GetId), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPedido(int id, [FromBody] PedidoRequest request)
    {
        await _pedidoService.AtualizarAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        await _pedidoService.RemoverAsync(id);
        return NoContent();
    }
}

