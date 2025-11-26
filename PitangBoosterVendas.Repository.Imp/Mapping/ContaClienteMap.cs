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
    public class ContaClienteMap : IEntityTypeConfiguration<ContaCliente>
    {
        public void Configure(EntityTypeBuilder<ContaCliente> builder)
        {
            builder.ToTable("tb_conta_cliente");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Saldo)
                .HasColumnName("saldo")
                .HasColumnType("decimal(18, 2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(e => e.LimiteCredito)
                .HasColumnName("limite_credito")
                .HasColumnType("decimal(18, 2)")
                .IsRequired()
                .HasDefaultValue(1000);

            builder.Property(e => e.DataCriacao)
                .HasColumnName("data_criacao")
                .IsRequired()
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            builder.HasOne(c => c.Cliente)
                .WithOne(cc => cc.ContaCliente)
                .HasForeignKey<ContaCliente>(cc => cc.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Pedidos)
                .WithOne(p => p.ContaCliente)
                .HasForeignKey(p => p.ContaClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
