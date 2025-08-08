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
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("tb_pedido");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataSolicitacao)
                .HasColumnName("data_solicitacao")
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(e => e.DataUltimaAtualizacao)
                .HasColumnName("data_ultima_atualizacao")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.Situacao)
                .HasColumnName("situacao")
                .HasColumnType("NVARCHAR(20)")
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}
