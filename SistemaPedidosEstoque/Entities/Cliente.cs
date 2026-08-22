namespace SistemaPedidosEstoque.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public string Telefone { get; set; } = string.Empty;
}
