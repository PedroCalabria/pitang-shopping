using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

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
                .IsRequired();

            builder.Property(e => e.ContaClienteId)
                .HasColumnName("conta_cliente_id")
                .IsRequired();

            builder.HasMany(p => p.ItensPedido)
               .WithOne(i => i.Pedido)
               .HasForeignKey(i => i.PedidoId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ContaCliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ContaClienteId);

            builder.HasMany(p => p.Pagamentos)
                .WithOne(pg => pg.Pedido)
                .HasForeignKey(pg => pg.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
