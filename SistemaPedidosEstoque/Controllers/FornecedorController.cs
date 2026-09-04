using Microsoft.AspNetCore.Mvc;
using SistemaPedidosEstoque.DTOs.Fornecedor;
using SistemaPedidosEstoque.Services;

namespace SistemaPedidosEstoque.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly FornecedorService _fornecedorService;

    public FornecedorController(FornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FornecedorResponse>>> Get()
    {
        var fornecedores = await _fornecedorService.ObterTodosAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FornecedorResponse>> Get(int id)
    {
        var fornecedor = await _fornecedorService.ObterPorIdAsync(id);
        if (fornecedor is null)
            return NotFound();

        return Ok(fornecedor);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] FornecedorRequest request)
    {
        var id = await _fornecedorService.AdicionarAsync(request);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] FornecedorRequest request)
    {
        await _fornecedorService.AtualizarAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _fornecedorService.RemoverAsync(id);
        return NoContent();
    }
}
