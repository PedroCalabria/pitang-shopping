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
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("tb_produto");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Preco)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(e => e.QuantidadeEstoque)
                .IsRequired();
        }
    }
}

//CREATE TABLE tb_produto (
//    id INT IDENTITY PRIMARY KEY,
//    nome NVARCHAR(100) NOT NULL,
//    preco DECIMAL(18, 2) NOT NULL,
//    quantidadeEstoque INT NOT NULL
//);
