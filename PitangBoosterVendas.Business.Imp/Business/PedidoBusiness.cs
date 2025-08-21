using log4net;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Entity.Enum;
using PitangBoosterVendas.Repository.IRepository;

namespace PitangBoosterVendas.Business.Imp.Business
{
    public class PedidoBusiness(IPedidoRepository _pedidoRepository) : IPedidoBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PedidoBusiness));

        public async Task AtualizarPedido(SituacaoPedidoEnum situacao, int id)
        {
            var pedidoExistente = await _pedidoRepository.FilterById(id);

            if (pedidoExistente == null)
            {
                _log.InfoFormat("Pedido não encontrado");
                throw new ArgumentException("Pedido não encontrado");
            }

            pedidoExistente.Situacao = situacao;
            pedidoExistente.DataUltimaAtualizacao = DateTime.Now;

            await _pedidoRepository.UpdateAsync(pedidoExistente);
        }

        public async Task<PedidoDTO> CadastrarPedido()
        {
            var pedido = await _pedidoRepository.InsertAsync(new Pedido
            {
                Situacao = SituacaoPedidoEnum.Pendente,
                DataSolicitacao = DateTime.Now,
                DataUltimaAtualizacao = DateTime.Now
            });

            return new PedidoDTO
            {
                Id = pedido.Id,
                DataSolicitacao = pedido.DataSolicitacao,
                DataUltimaAtualizacao = pedido.DataUltimaAtualizacao,
                Situacao = pedido.Situacao
            };
        }

        public async Task CancelarPedido(int id)
        {
            var pedidoExistente = await _pedidoRepository.FilterById(id);

            if (pedidoExistente == null)
            {
                _log.InfoFormat("Pedido não encontrado");
                throw new ArgumentException("Pedido não encontrado");
            }

            pedidoExistente.Situacao = SituacaoPedidoEnum.Cancelado;
            pedidoExistente.DataUltimaAtualizacao = DateTime.Now;

            await _pedidoRepository.UpdateAsync(pedidoExistente);
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

        public async Task<PedidoDTO> ObterPedido(int id)
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

        public async Task<decimal> ObterValorTotal(int id)
        {
            return await _pedidoRepository.ObterValorTotal(id);
        }

        public async Task<List<PedidoDTO>> ObterPedidosPorPeriodo(DateTime startDate, DateTime endDate)
        {
            return await _pedidoRepository.ObterPedidosPorPeriodo(startDate, endDate);
        }
    }
}
