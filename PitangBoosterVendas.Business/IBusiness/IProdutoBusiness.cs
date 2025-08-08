using PitangBoosterVendas.Entity.DTO;

namespace PitangBoosterVendas.Business.IBusiness
{
    public interface IProdutoBusiness
    {
        Task<List<ProdutoDTO>> ConsultarProdutos();
        Task<ProdutoDTO> ObterProduto(string id);
        Task CadastrarProduto(ProdutoDTO produto);
        Task AtualizarProduto(ProdutoDTO produto, int id);
        Task DeletarProduto(int id);
        Task<List<ProdutoDTO>> ConsultarProdutosEstoqueBaixo();
    }
}
