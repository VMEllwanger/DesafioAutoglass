using DesafioAutoglass.Produtos.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoglass.Produtos.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR SequenciaCodigoFornecedor");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.CNPJ)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.HasMany(c => c.Produtos)
              .WithOne(p => p.Fornecedor)
              .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
