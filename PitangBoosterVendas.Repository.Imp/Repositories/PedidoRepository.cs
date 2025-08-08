using Microsoft.EntityFrameworkCore;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class PedidoRepository(Context context) : RepositoryBase<Pedido>(context), IPedidoRepository
    {
        public Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao)
        {
            var query = from pedido in Entity
                        where ((int)pedido.Situacao) == situacao
                        select new PedidoDTO
                        {
                            Id = pedido.Id,
                            DataSolicitacao = pedido.DataSolicitacao,
                            DataUltimaAtualizacao = pedido.DataUltimaAtualizacao,
                            Situacao = pedido.Situacao
                        };

            return query.ToListAsync();
        }
    }
}
