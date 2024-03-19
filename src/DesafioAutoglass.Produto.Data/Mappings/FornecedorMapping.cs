using DesafioAutoglass.Produtos.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoglass.Produtos.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");

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

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("Date");

            builder.HasMany(c => c.Produtos)
              .WithOne(p => p.Fornecedor)
              .HasForeignKey(p => p.FornecedorId);

            builder.HasData(
                new Fornecedor("Emporio10 ME", "60585144000189") { },
                new Fornecedor("Pães e Doces Ltda", "69223161000140") { },
                new Fornecedor("Massa e Borda Pizzaria ME", "49526608000143") { }
                );
        }
    }
}
