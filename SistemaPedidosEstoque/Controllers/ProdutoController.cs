using Microsoft.AspNetCore.Mvc;
using SistemaPedidosEstoque.DTOs.Produto;
using SistemaPedidosEstoque.Services;

namespace SistemaPedidosEstoque.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutoController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> Get()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> Get(int id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] ProdutoRequest request)
    {
        var id = await _produtoService.AdicionarAsync(request);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProdutoRequest request)
    {
        await _produtoService.AtualizarAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _produtoService.RemoverAsync(id);
        return NoContent();
    }
}
