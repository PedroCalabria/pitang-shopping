using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class PagamentoPixMap : IEntityTypeConfiguration<PagamentoPix>
    {
        public void Configure(EntityTypeBuilder<PagamentoPix> builder)
        {
            builder.ToTable("tb_pagamento_pix");

            builder.Property(p => p.ChavePix)
                .HasColumnName("chavePix")
                .HasMaxLength(100)
                .IsRequired();
        }
    }

}
