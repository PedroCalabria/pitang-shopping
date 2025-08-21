using log4net;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository;

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
                var cartaoPagamento = p as CartaoPagamento;
                var pixPagamento = p as PixPagamento;
                var boletoPagamento = p as BoletoPagamento;
                return new PagamentoDTO
                {
                    Id = p.Id,
                    PedidoId = p.PedidoId,
                    Valor = p.Valor,
                    DataPagamento = p.DataPagamento,
                    TipoPagamento = p.TipoPagamento,
                    NumeroCartao = cartaoPagamento != null ? cartaoPagamento.NumeroCartao : null,
                    Parcelas = cartaoPagamento != null ? cartaoPagamento.Parcelas : (int?)null,
                    ChavePix = pixPagamento != null ? pixPagamento.ChavePix : null,
                    CodigoBarras = boletoPagamento != null ? boletoPagamento.CodigoBarras : null,
                    DataVencimento = boletoPagamento != null ? boletoPagamento.DataVencimento : (DateTime?)null
                };
            })];
        }

        public async Task<PagamentoDTO> ObterPagamentoPorPedido(int id)
        {
            var pagamento = await _pagamentoRepository.FilterById(id);

            if (pagamento == null)
            {
                _log.InfoFormat("Pagamento não encontrado");
                throw new ArgumentException("Pagamento não encontrado");
            }

            var cartaoPagamento = pagamento as CartaoPagamento;
            var pixPagamento = pagamento as PixPagamento;
            var boletoPagamento = pagamento as BoletoPagamento;

            return new PagamentoDTO
            {
                Id = pagamento.Id,
                PedidoId = pagamento.PedidoId,
                Valor = pagamento.Valor,
                DataPagamento = pagamento.DataPagamento,
                TipoPagamento = pagamento.TipoPagamento,
                NumeroCartao = cartaoPagamento != null ? cartaoPagamento.NumeroCartao : null,
                Parcelas = cartaoPagamento != null ? cartaoPagamento.Parcelas : (int?)null,
                ChavePix = pixPagamento != null ? pixPagamento.ChavePix : null,
                CodigoBarras = boletoPagamento != null ? boletoPagamento.CodigoBarras : null,
                DataVencimento = boletoPagamento != null ? boletoPagamento.DataVencimento : (DateTime?)null
            };
        }

        public async Task<PagamentoDTO> CadastrarPagamento(PagamentoDTO pagamento)
        {
            Pagamento novoPagamento;
            switch (pagamento.TipoPagamento)
            {
                case "Cartao":
                    novoPagamento = new CartaoPagamento
                    {
                        PedidoId = pagamento.PedidoId,
                        Valor = pagamento.Valor,
                        DataPagamento = pagamento.DataPagamento,
                        TipoPagamento = pagamento.TipoPagamento,
                        NumeroCartao = pagamento.NumeroCartao,
                        Parcelas = pagamento.Parcelas ?? 1
                    };
                    break;
                case "Pix":
                    novoPagamento = new PixPagamento
                    {
                        PedidoId = pagamento.PedidoId,
                        Valor = pagamento.Valor,
                        DataPagamento = pagamento.DataPagamento,
                        TipoPagamento = pagamento.TipoPagamento,
                        ChavePix = pagamento.ChavePix
                    };
                    break;
                case "Boleto":
                    novoPagamento = new BoletoPagamento
                    {
                        PedidoId = pagamento.PedidoId,
                        Valor = pagamento.Valor,
                        DataPagamento = pagamento.DataPagamento,
                        TipoPagamento = pagamento.TipoPagamento,
                        CodigoBarras = pagamento.CodigoBarras,
                        DataVencimento = pagamento.DataVencimento ?? DateTime.Now
                    };
                    break;
                default:
                    throw new ArgumentException("Tipo de pagamento inválido");
            }

            var pagamentoSalvo = await _pagamentoRepository.InsertAsync(novoPagamento);

            var cartaoPagamento = pagamentoSalvo as CartaoPagamento;
            var pixPagamento = pagamentoSalvo as PixPagamento;
            var boletoPagamento = pagamentoSalvo as BoletoPagamento;

            return new PagamentoDTO
            {
                Id = pagamentoSalvo.Id,
                PedidoId = pagamentoSalvo.PedidoId,
                Valor = pagamentoSalvo.Valor,
                DataPagamento = pagamentoSalvo.DataPagamento,
                TipoPagamento = pagamentoSalvo.TipoPagamento,
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

            pagamentoExistente.PedidoId = pagamento.PedidoId;
            pagamentoExistente.Valor = pagamento.Valor;
            pagamentoExistente.DataPagamento = pagamento.DataPagamento;
            pagamentoExistente.TipoPagamento = pagamento.TipoPagamento;

            if (pagamentoExistente is CartaoPagamento cartaoPagamento)
            {
                cartaoPagamento.NumeroCartao = pagamento.NumeroCartao;
                cartaoPagamento.Parcelas = pagamento.Parcelas ?? 1;
            }
            else if (pagamentoExistente is PixPagamento pixPagamento)
            {
                pixPagamento.ChavePix = pagamento.ChavePix;
            }
            else if (pagamentoExistente is BoletoPagamento boletoPagamento)
            {
                boletoPagamento.CodigoBarras = pagamento.CodigoBarras;
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
                var cartaoPagamento = p as CartaoPagamento;
                var pixPagamento = p as PixPagamento;
                var boletoPagamento = p as BoletoPagamento;
                return new PagamentoDTO
                {
                    Id = p.Id,
                    PedidoId = p.PedidoId,
                    Valor = p.Valor,
                    DataPagamento = p.DataPagamento,
                    TipoPagamento = p.TipoPagamento,
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


//public int Id { get; set; }
//public int PedidoId { get; set; }
//public decimal Valor { get; set; }
//public DateTime DataPagamento { get; set; }
//public string TipoPagamento { get; set; }
//public string? NumeroCartao { get; set; }
//public int? Parcelas { get; set; }
//public string? ChavePix { get; set; }
//public string? CodigoBarras { get; set; }
//public DateTime? DataVencimento { get; set; }