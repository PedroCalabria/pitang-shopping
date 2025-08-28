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

            builder.Property(p => p.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.DataPagamento)
                .HasColumnName("dataPagamento")
                .IsRequired();

            builder.Property(p => p.TipoPagamento)
                        .HasColumnName("tipoPagamento")
                        .HasMaxLength(50)
                        .IsRequired();

            // Configura o discriminator
            builder.HasDiscriminator<string>("TipoPagamento")
                   .HasValue<CartaoPagamento>("Cartao")
                   .HasValue<PixPagamento>("Pix")
                   .HasValue<BoletoPagamento>("Boleto");

            builder.HasOne(p => p.Pedido)
               .WithOne(p => p.Pagamento)
               .HasForeignKey<Pedido>(p => p.PagamentoId);
        }
    }

}
