using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;

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
        [HttpGet("obterPagamentoPorPedido")]
        public async Task<PagamentoDTO> ObterPagamentoPorPedido(int id)
        {
            var pagamento = await _pagamentoBusiness.ObterPagamentoPorPedido(id);
            return pagamento;
        }
        [HttpPost("cadastrarPagamento")]
        public async Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento)
        {
            return await _pagamentoBusiness.CadastrarPagamento(pagamento);
        }
        [HttpPut("atualizarPagamento")]
        public async Task AtualizarPagamento(PagamentoDTO pagamento)
        {
            await _pagamentoBusiness.AtualizarPagamento(pagamento);
        }
        [HttpDelete("cancelarPagamento")]
        public async Task CancelarPagamento(int id)
        {
            await _pagamentoBusiness.CancelarPagamento(id);
        }
    }
}
