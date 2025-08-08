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

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PedidoId)
                .IsRequired();

            builder.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(e => e.DataPagamento)
                .IsRequired();

            builder.Property(e => e.TipoPagamento)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.NumeroCartao)
                .HasMaxLength(16)
                .IsRequired(false);

            builder.Property(e => e.Parcelas)
                .IsRequired(false);

            builder.Property(e => e.ChavePix)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.CodigoBarras)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.DataVencimento)
                .IsRequired(false);

            builder.HasOne(p => p.Pedido)
                .WithOne(p => p.Pagamento)
                .HasForeignKey<Pagamento>(p => p.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
