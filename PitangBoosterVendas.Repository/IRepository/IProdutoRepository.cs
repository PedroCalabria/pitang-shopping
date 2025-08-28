using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository.Base;

namespace PitangBoosterVendas.Repository.IRepository
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<List<ProdutoDTO>> ConsultarProdutos();
        Task<ProdutoDTO> ObterProduto(int id);
        Task<ProdutoDTO> ObterProdutoPorNomeEPreco(string nome, decimal preco);
        Task<List<ProdutoDTO>> ConsultarProdutosEstoqueBaixo();
    }
}
