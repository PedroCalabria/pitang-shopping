using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class PedidoRepository(Context context) : RepositoryBase<Pedido>(context), IPedidoRepository
    {
    }
}
