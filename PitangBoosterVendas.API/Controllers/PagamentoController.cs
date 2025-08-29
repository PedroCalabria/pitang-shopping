using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Utils.Attributes;

namespace PitangBoosterVendas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController(IPagamentoBusiness _pagamentoBusiness): ControllerBase
    {
        [HttpGet("consultarPagamentos")]
        public async Task<List<PagamentoDTO>> ConsultarPagamentos()
        {
            var pagamentos = await _pagamentoBusiness.ConsultarPagamentos();
            return pagamentos;
        }

        [HttpGet("obterPagamentoPorId")]
        public async Task<PagamentoDTO> ObterPagamentoPorId(int id)
        {
            var pagamento = await _pagamentoBusiness.ObterPagamentoPorId(id);
            return pagamento;
        }

        [HttpGet("obterPagamentoPorPedido")]
        public async Task<List<PagamentoDTO>> ObterPagamentoPorPedido(int id)
        {
            var pagamento = await _pagamentoBusiness.ObterPagamentoPorPedido(id);
            return pagamento;
        }

        [HttpPost("cadastrarPagamento")]
        [TransactionRequired]
        public async Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento, string tipoPagamento)
        {
            return await _pagamentoBusiness.CadastrarPagamento(pagamento, tipoPagamento);
        }
        [HttpPut("atualizarPagamento")]
        [TransactionRequired]
        public async Task AtualizarPagamento(PagamentoDTO pagamento)
        {
            await _pagamentoBusiness.AtualizarPagamento(pagamento);
        }
        [HttpDelete("cancelarPagamento")]
        [TransactionRequired]
        public async Task CancelarPagamento(int id)
        {
            await _pagamentoBusiness.CancelarPagamento(id);
        }

        [HttpGet("consultarPagamentosPorTipo")]
        public async Task<List<PagamentoDTO>> ConsultarPagamentosPorTipo(string tipoPagamento)
        {
            var pagamentos = await _pagamentoBusiness.ConsultarPagamentosPorTipo(tipoPagamento);
            return pagamentos;
        }
    }
}
