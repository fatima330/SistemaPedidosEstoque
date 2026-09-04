namespace SistemaPedidosEstoque.Entities;

public class Produto
{
    public int Id { get; set; }
    public int? FornecedorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal QtdeEstoque { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; }
}
