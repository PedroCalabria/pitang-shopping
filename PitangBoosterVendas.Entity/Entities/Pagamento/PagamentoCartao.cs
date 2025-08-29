namespace PitangBoosterVendas.Entity.Entities
{
    public class PagamentoCartao : Pagamento
    {
        public string NumeroCartao { get; set; }
        public int Parcelas { get; set; }
    }
}
