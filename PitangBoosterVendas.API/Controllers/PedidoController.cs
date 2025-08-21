using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Entity.Enum;

namespace PitangBoosterVendas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController(IPedidoBusiness _pedidoBusiness): ControllerBase
    {
        [HttpGet("consultarPedidos")]
        public async Task<List<PedidoDTO>> ConsultarPedidos()
        {
            var produtos = await _pedidoBusiness.ConsultarPedidos();

            return produtos;
        }

        [HttpGet("obterPedido")]
        public async Task<PedidoDTO> ObterPedido(int id)
        {
            var produto = await _pedidoBusiness.ObterPedido(id);

            return produto;
        }

        [HttpPost("cadastrarPedido")]
        public async Task<PedidoDTO> CadastrarPedido()
        {
            return await _pedidoBusiness.CadastrarPedido();
        }

        [HttpPut("atualizarPedido")]
        public async Task AtualizarPedido(SituacaoPedidoEnum situacao, int id)
        {
            await _pedidoBusiness.AtualizarPedido(situacao, id);
        }

        [HttpDelete("cancelarPedido")]
        public async Task CancelarPedido(int id)
        {
            await _pedidoBusiness.CancelarPedido(id);
        }

        [HttpGet("consultarPedidosPorSituacao")]
        public async Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao)
        {
            return await _pedidoBusiness.ConsultarPedidosPorSituacao(situacao);
        }

        [HttpGet("obterValorTotal")]
        public async Task<decimal> ObterValorTotal(int id)
        {
            return await _pedidoBusiness.ObterValorTotal(id);
        }

        [HttpGet("obterPedidosPorPeriodo")]
        public async Task<List<PedidoDTO>> ObterPedidosPorPeriodo(DateTime startDate, DateTime endDate)
        {
            return await _pedidoBusiness.ObterPedidosPorPeriodo(startDate, endDate);
        }
    }
}
