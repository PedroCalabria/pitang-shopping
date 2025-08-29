using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class PagamentoMap : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("tb_pagamento");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.DataPagamento)
                .HasColumnName("dataPagamento")
                .IsRequired();

            builder.HasOne(p => p.Pedido)
                   .WithOne(p => p.Pagamento)
                   .HasForeignKey<Pedido>(p => p.PagamentoId);
        }
    }
}
