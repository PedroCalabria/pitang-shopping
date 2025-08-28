namespace PitangBoosterVendas.Entity.Entities
{
    public class CartaoPagamento : Pagamento
    {
        public string NumeroCartao { get; set; }
        public int Parcelas { get; set; }
    }
}
