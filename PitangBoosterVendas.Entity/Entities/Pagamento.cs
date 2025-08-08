namespace PitangBoosterVendas.Entity.Entities
{
    public class Pagamento: IEntity
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string TipoPagamento { get; set; }

        // Para Cartão
        public string? NumeroCartao { get; set; } 
        public int? Parcelas { get; set; }

        // Para Pix
        public string? ChavePix { get; set; }

        // Para Boleto
        public string? CodigoBarras { get; set; }
        public DateTime? DataVencimento { get; set; }

        public Pedido Pedido { get; set; }

    }
}
