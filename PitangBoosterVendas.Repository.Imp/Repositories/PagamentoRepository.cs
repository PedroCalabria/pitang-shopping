using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class PagamentoRepository(Context context) : RepositoryBase<Pagamento>(context), IPagamentoRepository
    {
    }
}
