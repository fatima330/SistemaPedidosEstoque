namespace SistemaPedidosEstoque.DTOs.Pedido
{
    public class PedidoResponse
    {
        public int IdPedido { get; set; }
        public int? IdCliente { get; set; }
        public int? IdProduto { get; set; }
        public int? Quantidade { get; set; }
    }
}
