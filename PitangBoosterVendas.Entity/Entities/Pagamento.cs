namespace PitangBoosterVendas.Entity.Entities
{
    public abstract class Pagamento : IEntity
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string TipoPagamento { get; set; }

        public Pedido Pedido { get; set; }
    }

    public class CartaoPagamento : Pagamento
    {
        public string NumeroCartao { get; set; }
        public int Parcelas { get; set; }
    }

    public class PixPagamento : Pagamento
    {
        public string ChavePix { get; set; }
    }

    public class BoletoPagamento : Pagamento
    {
        public string CodigoBarras { get; set; }
        public DateTime DataVencimento { get; set; }
    }


}
