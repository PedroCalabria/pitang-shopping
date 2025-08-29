using log4net;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository;
using System.Globalization;

namespace PitangBoosterVendas.Business.Imp.Business
{
    public class PagamentoBusiness(IPagamentoRepository _pagamentoRepository) : IPagamentoBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PagamentoBusiness));

        public async Task<List<PagamentoDTO>> ConsultarPagamentos()
        {
            var pagamentos = await _pagamentoRepository.ObterTodosAsNoTracking();

            return [.. pagamentos.Select(p =>
            {
                var cartaoPagamento = p as PagamentoCartao;
                var pixPagamento = p as PagamentoPix;
                var boletoPagamento = p as PagamentoBoleto;
                return new PagamentoDTO
                {
                    Id = p.Id,
                    Valor = p.Valor,
                    DataPagamento = p.DataPagamento,
                    NumeroCartao = cartaoPagamento != null ? cartaoPagamento.NumeroCartao : null,
                    Parcelas = cartaoPagamento != null ? cartaoPagamento.Parcelas : (int?)null,
                    ChavePix = pixPagamento != null ? pixPagamento.ChavePix : null,
                    CodigoBarras = boletoPagamento != null ? boletoPagamento.CodigoBarras : null,
                    DataVencimento = boletoPagamento != null ? boletoPagamento.DataVencimento : (DateTime?)null
                };
            })];
        }

        public async Task<PagamentoDTO> ObterPagamentoPorId(int id)
        {
            var pagamento = await _pagamentoRepository.FilterById(id);

            if (pagamento == null)
            {
                _log.InfoFormat("Pagamento não encontrado");
                throw new ArgumentException("Pagamento não encontrado");
            }

            var cartaoPagamento = pagamento as PagamentoCartao;
            var pixPagamento = pagamento as PagamentoPix;
            var boletoPagamento = pagamento as PagamentoBoleto;

            return new PagamentoDTO
            {
                Id = pagamento.Id,
                Valor = pagamento.Valor,
                DataPagamento = pagamento.DataPagamento,
                NumeroCartao = cartaoPagamento != null ? cartaoPagamento.NumeroCartao : null,
                Parcelas = cartaoPagamento != null ? cartaoPagamento.Parcelas : (int?)null,
                ChavePix = pixPagamento != null ? pixPagamento.ChavePix : null,
                CodigoBarras = boletoPagamento != null ? boletoPagamento.CodigoBarras : null,
                DataVencimento = boletoPagamento != null ? boletoPagamento.DataVencimento : (DateTime?)null
            };
        }

        public async Task<List<PagamentoDTO>> ObterPagamentoPorPedido(int id)
        {
            var pagamentos = await _pagamentoRepository.ObterPagamentoPorPedido(id);

            if (pagamentos == null || pagamentos.Count == 0)
            {
                _log.InfoFormat("Pagamento não encontrado");
                throw new ArgumentException("Pagamento não encontrado");
            }

            return [.. pagamentos.Select(p =>
            {
                var cartaoPagamento = p as PagamentoCartao;
                var pixPagamento = p as PagamentoPix;
                var boletoPagamento = p as PagamentoBoleto;
                return new PagamentoDTO
                {
                    Id = p.Id,
                    Valor = p.Valor,
                    DataPagamento = p.DataPagamento,
                    NumeroCartao = cartaoPagamento?.NumeroCartao,
                    Parcelas = cartaoPagamento ?.Parcelas,
                    ChavePix = pixPagamento?.ChavePix,
                    CodigoBarras = boletoPagamento?.CodigoBarras,
                    DataVencimento = boletoPagamento?.DataVencimento
                };
            })];
        }

        public async Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento, string tipoPagamento)
        {
            Pagamento novoPagamento = tipoPagamento switch
            {
                "Cartao" => new PagamentoCartao
                {
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    NumeroCartao = pagamento.NumeroCartao,
                    Parcelas = pagamento.Parcelas ?? 1
                },
                "Pix" => new PagamentoPix
                {
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    ChavePix = pagamento.ChavePix
                },
                "Boleto" => new PagamentoBoleto
                {
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    CodigoBarras = pagamento.CodigoBarras,
                    DataVencimento = pagamento.DataVencimento ?? DateTime.Now
                },
                _ => throw new ArgumentException("Tipo de pagamento inválido"),
            };
            var pagamentoSalvo = await _pagamentoRepository.InsertAsync(novoPagamento);

            var cartaoPagamento = pagamentoSalvo as PagamentoCartao;
            var pixPagamento = pagamentoSalvo as PagamentoPix;
            var boletoPagamento = pagamentoSalvo as PagamentoBoleto;

            return new PagamentoDTO
            {
                Id = pagamentoSalvo.Id,
                Valor = pagamentoSalvo.Valor,
                DataPagamento = pagamentoSalvo.DataPagamento,
                NumeroCartao = cartaoPagamento?.NumeroCartao,
                Parcelas = cartaoPagamento?.Parcelas,
                ChavePix = pixPagamento?.ChavePix,
                CodigoBarras = boletoPagamento?.CodigoBarras,
                DataVencimento = boletoPagamento?.DataVencimento
            };
        }

        public async Task AtualizarPagamento(PagamentoDTO pagamento)
        {
            var pagamentoExistente = await _pagamentoRepository.FilterById(pagamento.Id);

            if (pagamentoExistente == null)
            {
                _log.InfoFormat("Pagamento não encontrado");
                throw new ArgumentException("Pagamento não encontrado");
            }

            pagamentoExistente.Valor = pagamento.Valor;
            pagamentoExistente.DataPagamento = pagamento.DataPagamento;

            if (pagamentoExistente is PagamentoCartao cartaoPagamento)
            {
                cartaoPagamento.NumeroCartao = pagamento.NumeroCartao!;
                cartaoPagamento.Parcelas = pagamento.Parcelas ?? 1;
            }
            else if (pagamentoExistente is PagamentoPix pixPagamento)
            {
                pixPagamento.ChavePix = pagamento.ChavePix!;
            }
            else if (pagamentoExistente is PagamentoBoleto boletoPagamento)
            {
                boletoPagamento.CodigoBarras = pagamento.CodigoBarras!;
                boletoPagamento.DataVencimento = pagamento.DataVencimento ?? DateTime.Now;
            }

            await _pagamentoRepository.UpdateAsync(pagamentoExistente);
        }

        public async Task CancelarPagamento(int id)
        {
            var pagamentoExistente = await _pagamentoRepository.FilterById(id);

            if (pagamentoExistente == null)
            {
                _log.InfoFormat("Pagamento não encontrado");
                throw new ArgumentException("Pagamento não encontrado");
            }

            //TODO: Implementar lógica de cancelamento
        }

        public async Task<List<PagamentoDTO>> ConsultarPagamentosPorTipo(string tipoPagamento)
        {
            var pagamentos = await _pagamentoRepository.ConsultarPagamentosPorTipo(tipoPagamento);

            return [.. pagamentos.Select(p =>
            {
                var cartaoPagamento = p as PagamentoCartao;
                var pixPagamento = p as PagamentoPix;
                var boletoPagamento = p as PagamentoBoleto;
                return new PagamentoDTO
                {
                    Id = p.Id,
                    Valor = p.Valor,
                    DataPagamento = p.DataPagamento,
                    NumeroCartao = cartaoPagamento != null ? cartaoPagamento.NumeroCartao : null,
                    Parcelas = cartaoPagamento != null ? cartaoPagamento.Parcelas : (int?)null,
                    ChavePix = pixPagamento != null ? pixPagamento.ChavePix : null,
                    CodigoBarras = boletoPagamento != null ? boletoPagamento.CodigoBarras : null,
                    DataVencimento = boletoPagamento != null ? boletoPagamento.DataVencimento : (DateTime?)null
                };
            })];
        }
    }
}
