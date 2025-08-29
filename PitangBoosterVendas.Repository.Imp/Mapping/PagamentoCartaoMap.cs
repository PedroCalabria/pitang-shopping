using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class PagamentoCartaoMap : IEntityTypeConfiguration<PagamentoCartao>
    {
        public void Configure(EntityTypeBuilder<PagamentoCartao> builder)
        {
            builder.ToTable("tb_pagamento_cartao");

            builder.Property(p => p.NumeroCartao)
                .HasColumnName("numeroCartao")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Parcelas)
                .HasColumnName("parcelas")
                .IsRequired();
        }
    }
}
