using log4net;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Business.Imp.Business
{
    public class ProdutoBusiness(IProdutoRepository _produtoRepository) : IProdutoBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ProdutoBusiness));
        public async Task<List<ProdutoDTO>> ConsultarProdutos()
        {
            var produtos = await _produtoRepository.ConsultarProdutos();

            return produtos;
        }

        public async Task<ProdutoDTO> ObterProduto(string id)
        {
            var produto = await _produtoRepository.ObterProduto(int.Parse(id));

            return produto;
        }

        public async Task CadastrarProduto(ProdutoDTO produto)
        {
            var produtoExistente = await _produtoRepository.ObterProdutoPorNomeEPreco(produto.Nome, produto.Preco);

            if (produtoExistente != null)
            {
                var produtoEntity = new Produto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    QuantidadeEstoque = produtoExistente.QuantidadeEstoque + produto.QuantidadeEstoque
                };

                await _produtoRepository.UpdateAsync(produtoEntity);
            }
            else
            {
                await _produtoRepository.InsertAsync(new Produto
                {
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    QuantidadeEstoque = produto.QuantidadeEstoque
                });
            }
        }

        public async Task AtualizarProduto(ProdutoDTO produto, int id)
        {
            var produtoExistente = await _produtoRepository.FilterById(id);

            if (produtoExistente == null)
            {
                _log.InfoFormat("Produto não encontrado");
                throw new ArgumentException("Produto não encontrado");
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;

            await _produtoRepository.UpdateAsync(produtoExistente);
        }

        public async Task DeletarProduto(int id)
        {
            var produtoExistente = await _produtoRepository.FilterById(id);

            if (produtoExistente == null)
            {
                _log.InfoFormat("Produto não encontrado");
                throw new ArgumentException("Produto não encontrado");
            }

            await _produtoRepository.RemoveAsync(produtoExistente);
        }

        public async Task<List<ProdutoDTO>> ConsultarProdutosEstoqueBaixo()
        {
            return await _produtoRepository.ConsultarProdutosEstoqueBaixo();
        }
    }
}
