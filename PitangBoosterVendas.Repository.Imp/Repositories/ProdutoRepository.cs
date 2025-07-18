using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class ProdutoRepository(Context context) : RepositoryBase<Produto>(context), IProdutoRepository
    {
    }
}
