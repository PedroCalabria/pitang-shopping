using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PitangBoosterVendas.Entity.Entities;

namespace PitangBoosterVendas.Repository.Imp.Mapping
{
    public class ItemPedidoMap : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("tb_itemPedido");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.PedidoId)
                .HasColumnName("pedidoId")
                .IsRequired();

            builder.Property(e => e.ProdutoId)
                .HasColumnName("produtoId")
                .IsRequired();

            builder.Property(e => e.Quantidade)
                .HasColumnName("quantidade")
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
