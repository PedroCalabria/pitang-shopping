using Microsoft.EntityFrameworkCore;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.Imp.Repositories.Base;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class PagamentoRepository(Context context) : RepositoryBase<Pagamento>(context), IPagamentoRepository
    {
        public async Task<List<Pagamento>> ConsultarPagamentosPorTipo(string tipoPagamento)
        {
            var pagamento = tipoPagamento switch
            {
                "Cartao" => (await Entity.OfType<PagamentoCartao>().ToListAsync()).Cast<Pagamento>().ToList(),
                "Pix" => (await Entity.OfType<PagamentoPix>().ToListAsync()).Cast<Pagamento>().ToList(),
                "Boleto" => (await Entity.OfType<PagamentoBoleto>().ToListAsync()).Cast<Pagamento>().ToList(),
                _ => []
            };

            return pagamento;
        }

        public Task<List<Pagamento>> ObterPagamentoPorPedido(int idPedido)
        {
            var query = from pagamento in Entity

                        join pedido in context.Pedido
                        on pagamento.Id equals pedido.PagamentoId

                        where pedido.Id == idPedido
                        select pagamento;

            return query.ToListAsync();
        }
    }
}
