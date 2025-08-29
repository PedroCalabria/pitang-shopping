namespace PitangBoosterVendas.Entity.Entities
{
    public abstract class Pagamento : IEntity
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }

        public Pedido? Pedido { get; set; }
    }
}
