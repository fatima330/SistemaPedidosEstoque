using SistemaPedidosEstoque.DTOs.Pedido;
using SistemaPedidosEstoque.Entities;
using SistemaPedidosEstoque.Interfaces;

namespace SistemaPedidosEstoque.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<PedidoResponse>> ObterTodosAsync()
        {
            var pedidos = await _pedidoRepository.ObterTodosAsync();
            return pedidos.Select(p => new PedidoResponse
            {
                IdPedido = p.IdPedido,
                IdCliente = p.IdCliente,
                IdProduto = p.IdProduto,
                Quantidade = p.Quantidade
            });
        }

        public async Task<PedidoResponse?> ObterPorIdAsync(int id)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(id);
            if (pedido is null)
                return null;

            return new PedidoResponse
            {
                IdPedido = pedido.IdPedido,
                IdCliente = pedido.IdCliente,
                IdProduto = pedido.IdProduto,
                Quantidade = pedido.Quantidade
            };
        }

        public async Task<int> AdicionarAsync(PedidoRequest request)
        {
            var pedido = new Pedido
            {
                IdCliente = request.IdCliente,
                IdProduto = request.IdProduto,
                Quantidade = request.Quantidade
            };

            return await _pedidoRepository.AdicionarAsync(pedido);
        }


        public async Task AtualizarAsync(int id, PedidoRequest request)
        {
            var pedido = new Pedido
            {
                IdPedido = id,
                IdCliente = request.IdCliente,
                IdProduto = request.IdProduto,
                Quantidade = request.Quantidade
            };

            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task RemoverAsync(int id)
        {
            await _pedidoRepository.RemoverAsync(id);
        }

    }
}
