using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;

namespace PitangBoosterVendas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController(IProdutoBusiness _produtoBusiness): ControllerBase
    {
        [HttpGet("consultarProdutos")]
        public async Task<List<ProdutoDTO>> ConsultarProdutos()
        {
            var produtos = await _produtoBusiness.ConsultarProdutos();

            return produtos;
        }

        [HttpGet("obterProduto")]
        public async Task<ProdutoDTO> ObterProduto(string id)
        {
            var produto = await _produtoBusiness.ObterProduto(id);

            return produto;
        }

        [HttpPost("cadastrarProduto")]
        public async Task CadastrarProduto(ProdutoDTO produto)
        {
            await _produtoBusiness.CadastrarProduto(produto);
        }

        [HttpPut("atualizarProduto")]
        public async Task AtualizarProduto(ProdutoDTO produto, int id)
        {
            await _produtoBusiness.AtualizarProduto(produto, id);
        }

        [HttpDelete("deletarProduto")]
        public async Task DeletarProduto(int id)
        {
            await _produtoBusiness.DeletarProduto(id);
        }

        [HttpGet("consultarProdutosEstoqueBaixo")]
        public async Task<List<ProdutoDTO>> ConsultarProdutosEstoqueBaixo()
        {
            return await _produtoBusiness.ConsultarProdutosEstoqueBaixo();
        }
    }
}
