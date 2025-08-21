using PitangBoosterVendas.Entity.DTO;

namespace PitangBoosterVendas.Business.IBusiness
{
    public interface IPagamentoBusiness
    {
        Task<List<PagamentoDTO>> ConsultarPagamentos();
        Task<PagamentoDTO> ObterPagamentoPorPedido(int id);
        Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento);
        Task AtualizarPagamento(PagamentoDTO pagamento);
        Task CancelarPagamento(int id);
    }
}
