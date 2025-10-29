using PitangBoosterVendas.Entity.Enum;

namespace PitangBoosterVendas.Entity.Entities
{
    public class Pedido: IEntity
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public SituacaoPedidoEnum Situacao { get; set; }

        public int ContaClienteId { get; set; }
        public ContaCliente ContaCliente { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } = [];
        public NotaFiscal NotaFiscal { get; set; }
        public List<Pagamento> Pagamentos { get; set; } = [];
    }
}
