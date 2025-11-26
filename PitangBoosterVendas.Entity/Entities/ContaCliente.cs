using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.Entities
{
    public class ContaCliente: IEntity
    {
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public decimal LimiteCredito { get; set; }
        public DateTime DataCriacao { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public List<Pedido> Pedidos { get; set; } = [];
    }
}
