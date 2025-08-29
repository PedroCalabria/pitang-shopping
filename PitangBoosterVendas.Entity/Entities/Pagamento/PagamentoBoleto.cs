namespace PitangBoosterVendas.Entity.Entities
{
    public class PagamentoBoleto : Pagamento
    {
        public string CodigoBarras { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
