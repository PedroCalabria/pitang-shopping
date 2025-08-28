using PitangBoosterVendas.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.Entities
{
    public class Pedido: IEntity
    {
        public int Id { get; set; }
        public int? PagamentoId { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public SituacaoPedidoEnum Situacao { get; set; }

        public Pagamento? Pagamento { get; set; }
        public List<ItemPedido> ItensPedido { get; set; } = [];
    }
}
