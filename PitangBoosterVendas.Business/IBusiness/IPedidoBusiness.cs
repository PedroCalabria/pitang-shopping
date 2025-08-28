using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Entity.Enum;

namespace PitangBoosterVendas.Business.IBusiness
{
    public interface IPedidoBusiness
    {
        Task<List<PedidoDTO>> ConsultarPedidos();
        Task<PedidoDTO> ObterPedido(int id);
        Task<PedidoDTO> CadastrarPedido();
        Task AtualizarPedido(SituacaoPedidoEnum situacao, int id);
        Task CancelarPedido(int id);
        Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao);
        Task<decimal> ObterValorTotal(int id);
        Task<List<PedidoDTO>> ObterPedidosPorPeriodo(DateTime startDate, DateTime endDate);
    }
}
