using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class PagamentoBoletoMap : IEntityTypeConfiguration<PagamentoBoleto>
    {
        public void Configure(EntityTypeBuilder<PagamentoBoleto> builder)
        {
            builder.ToTable("tb_pagamento_boleto");

            builder.Property(p => p.CodigoBarras)
                .HasColumnName("codigoBarras")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.DataVencimento)
                .HasColumnName("dataVencimento")
                .IsRequired();
        }
    }
}
