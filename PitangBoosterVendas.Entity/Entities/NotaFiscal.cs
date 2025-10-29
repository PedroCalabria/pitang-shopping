using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.Entities
{
    public class NotaFiscal: IEntity
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal ValorTotal { get; set; }
        public int NumeroNota { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
