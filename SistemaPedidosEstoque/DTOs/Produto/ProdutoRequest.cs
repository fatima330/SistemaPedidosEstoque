namespace SistemaPedidosEstoque.DTOs.Produto;

public class ProdutoRequest
{
    public int? FornecedorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal QtdeEstoque { get; set; }
}
