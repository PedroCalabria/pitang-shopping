using PitangBoosterVendas.Entity.DTO;

namespace PitangBoosterVendas.Business.IBusiness
{
    public interface IPedidoBusiness
    {
        Task<List<PedidoDTO>> ConsultarPedidos();
        Task<PedidoDTO> ObterPedido(string id);
        Task CadastrarPedido(PedidoDTO pedido);
        Task AtualizarPedido(PedidoDTO pedido, int id);
        Task CancelarPedido(int id);
        Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao);
    }
}
