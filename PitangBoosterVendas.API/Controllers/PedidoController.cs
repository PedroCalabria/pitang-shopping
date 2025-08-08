using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Entity.DTO;

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
        public async Task<PedidoDTO> ObterPedido(string id)
        {
            var produto = await _pedidoBusiness.ObterPedido(id);

            return produto;
        }

        [HttpPost("cadastrarPedido")]
        public async Task CadastrarPedido(PedidoDTO pedido)
        {
            await _pedidoBusiness.CadastrarPedido(pedido);
        }

        [HttpPut("atualizarPedido")]
        public async Task AtualizarPedido(PedidoDTO pedido, int id)
        {
            await _pedidoBusiness.AtualizarPedido(pedido, id);
        }

        [HttpDelete("cancelarPedido")]
        public async Task CancelarPedido(int id)
        {
            await _pedidoBusiness.CancelarPedido(id);
        }

        [HttpGet("consultarPedidosCancelados")]
        public async Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao)
        {
            return await _pedidoBusiness.ConsultarPedidosPorSituacao(situacao);
        }
    }
}
