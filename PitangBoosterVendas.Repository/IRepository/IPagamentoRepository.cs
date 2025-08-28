using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository.Base;

namespace PitangBoosterVendas.Repository.IRepository
{
    public interface IPagamentoRepository : IRepositoryBase<Pagamento>
    {
        Task<List<Pagamento>> ConsultarPagamentosPorTipo(string tipoPagamento);
        Task<List<Pagamento>> ObterPagamentoPorPedido(int idPedido);
    }
}
