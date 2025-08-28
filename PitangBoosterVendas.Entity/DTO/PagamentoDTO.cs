using PitangBoosterVendas.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.DTO
{
    public class PagamentoDTO
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string TipoPagamento { get; set; }
        public string? NumeroCartao { get; set; }
        public int? Parcelas { get; set; }
        public string? ChavePix { get; set; }
        public string? CodigoBarras { get; set; }
        public DateTime? DataVencimento { get; set; }
    }
}
