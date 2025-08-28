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

        public Task<decimal> ObterValorTotal(int id)
        {
            var query = from pedido in Entity
                        join itemPedido in Context.ItemPedido
                        on pedido.Id equals itemPedido.PedidoId
                        join produto in Context.Produto
                        on itemPedido.ProdutoId equals produto.Id
                        where pedido.Id == id
                        select itemPedido.Quantidade * produto.Preco;

            return query.SumAsync();
        }

        public Task<List<PedidoDTO>> ObterPedidosPorPeriodo(DateTime startDate, DateTime endDate)
        {
            var query = from pedido in Entity
                        where pedido.DataSolicitacao >= startDate && pedido.DataSolicitacao <= endDate
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
