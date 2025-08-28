namespace PitangBoosterVendas.Entity.Entities
{
    public class BoletoPagamento : Pagamento
    {
        public string CodigoBarras { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
