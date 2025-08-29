using PitangBoosterVendas.Entity.DTO;

namespace PitangBoosterVendas.Business.IBusiness
{
    public interface IPagamentoBusiness
    {
        Task<List<PagamentoDTO>> ConsultarPagamentos();
        Task<PagamentoDTO> ObterPagamentoPorId(int id);
        Task<List<PagamentoDTO>> ObterPagamentoPorPedido(int id);
        Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento, string tipoPagamento);
        Task AtualizarPagamento(PagamentoDTO pagamento);
        Task CancelarPagamento(int id);
        Task<List<PagamentoDTO>> ConsultarPagamentosPorTipo(string tipoPagamento);
    }
}
