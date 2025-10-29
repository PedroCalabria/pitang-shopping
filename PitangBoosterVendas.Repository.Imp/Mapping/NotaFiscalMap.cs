using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class NotaFiscalMap : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.ToTable("tb_nota_fiscal");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataEmissao)
                .HasColumnName("data_emissao")
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ValorTotal)
                .HasColumnName("valor_total")
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(e => e.NumeroNota)
                .HasColumnName("numero")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PedidoId)
                .HasColumnName("pedido_id")
                .IsRequired();

            builder.HasOne(nf => nf.Pedido)
                .WithOne(p => p.NotaFiscal)
                .HasForeignKey<NotaFiscal>(nf => nf.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
