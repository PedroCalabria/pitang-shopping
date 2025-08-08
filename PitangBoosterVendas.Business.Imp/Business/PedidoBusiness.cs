using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Business.Imp.Business
{
    public class PedidoBusiness(IPedidoRepository _pedidoRepository) : IPedidoBusiness
    {
        public Task AtualizarPedido(PedidoDTO pedido, int id)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarPedido(PedidoDTO pedido)
        {
            throw new NotImplementedException();
        }

        public Task CancelarPedido(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PedidoDTO>> ConsultarPedidos()
        {
            var pedidos = await _pedidoRepository.ObterTodosAsNoTracking();

            var pedidosDtos = pedidos.Select(p => new PedidoDTO
            {
                Id = p.Id,
                DataSolicitacao = p.DataSolicitacao,
                DataUltimaAtualizacao = p.DataUltimaAtualizacao,
                Situacao = p.Situacao,
            }).ToList();

            return pedidosDtos;
        }

        public async Task<List<PedidoDTO>> ConsultarPedidosPorSituacao(int situacao)
        {
            return await _pedidoRepository.ConsultarPedidosPorSituacao(situacao);
        }

        public async Task<PedidoDTO> ObterPedido(string id)
        {
            var pedido = await _pedidoRepository.FilterById(id);

            return new PedidoDTO
            {
                Id = pedido.Id,
                DataSolicitacao = pedido.DataSolicitacao,
                DataUltimaAtualizacao = pedido.DataUltimaAtualizacao,
                Situacao = pedido.Situacao,
            };
        }
    }
}
