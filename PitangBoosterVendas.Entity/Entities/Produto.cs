namespace PitangBoosterVendas.Entity.Entities
{
    public class Produto : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } = [];
    }
}
