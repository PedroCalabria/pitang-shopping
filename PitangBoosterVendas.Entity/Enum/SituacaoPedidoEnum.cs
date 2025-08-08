using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.Enum
{
    public enum SituacaoPedidoEnum
    {
        [Description("Pagamento pendente")]
        Pendente = 1,
        [Description("Pedido em preparacao")]
        EmPreparacao,
        [Description("Pedido enviado")]
        Enviado,
        [Description("Pedido entregue")]
        Entregue,
        [Description("Pedido pendente")]
        Cancelado
    }
}
