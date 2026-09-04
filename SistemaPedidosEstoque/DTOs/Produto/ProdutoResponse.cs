namespace SistemaPedidosEstoque.DTOs.Produto;

public class ProdutoResponse
{
    public int Id { get; set; }
    public int? FornecedorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal QtdeEstoque { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
}
