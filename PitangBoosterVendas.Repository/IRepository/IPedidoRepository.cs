using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository.Base;

namespace PitangBoosterVendas.Repository.IRepository
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao);
        Task<decimal> ObterValorTotal(int id);
        Task<List<PedidoDTO>> ObterPedidosPorPeriodo(DateTime startDate, DateTime endDate);
    }
}
