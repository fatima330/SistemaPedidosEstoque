namespace SistemaPedidosEstoque.DTOs.Pedido
{
    public class PedidoRequest
    {
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
