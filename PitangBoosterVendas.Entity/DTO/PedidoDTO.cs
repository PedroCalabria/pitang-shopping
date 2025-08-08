using PitangBoosterVendas.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.DTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public SituacaoPedidoEnum Situacao { get; set; }
    }
}
