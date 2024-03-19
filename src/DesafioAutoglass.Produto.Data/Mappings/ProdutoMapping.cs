using DesafioAutoglass.Produtos.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoglass.Produtos.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR SequenciaCodigoProdutos");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Situacao)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.DataDeFabricacao)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(p => p.DataDeValidade)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.ToTable("Produtos");
        }
    }
}
