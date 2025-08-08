using Microsoft.EntityFrameworkCore;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class ProdutoRepository(Context context) : RepositoryBase<Produto>(context), IProdutoRepository
    {
        public Task<List<ProdutoDTO>> ConsultarProdutos()
        {
            var query = Entity.Select(e => new ProdutoDTO
            {
                Nome = e.Nome,
                Preco = e.Preco,
                QuantidadeEstoque = e.QuantidadeEstoque
            });

            return query.ToListAsync();
        }

        public Task<ProdutoDTO> ObterProduto(int id)
        {
            var query = Entity
                .Where(e => e.Id == id)
                .Select(e => new ProdutoDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Preco = e.Preco,
                    QuantidadeEstoque = e.QuantidadeEstoque
                }).FirstOrDefaultAsync();

            return query;
        }

        public Task<ProdutoDTO> ObterProdutoPorNomeEPreco(string nome, decimal preco)
        {
            var query = Entity
                .Where(e => e.Nome == nome && e.Preco == preco)
                .Select(e => new ProdutoDTO
                {
                    Nome = e.Nome,
                    Preco = e.Preco,
                    QuantidadeEstoque = e.QuantidadeEstoque
                })
                .FirstOrDefaultAsync();

            return query;
        }

        public Task<List<ProdutoDTO>> ConsultarProdutosEstoqueBaixo()
        {
            var query = Entity
                .Where(e => e.QuantidadeEstoque < 20)
                .Select(e => new ProdutoDTO
                {
                    Nome = e.Nome,
                    Preco = e.Preco,
                    QuantidadeEstoque = e.QuantidadeEstoque
                });

            return query.ToListAsync();
        }
    }
}
