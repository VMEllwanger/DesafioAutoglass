using DesafioAutoglass.Core.DomainDeObjetos;
using DesafioAutoglass.Produtos.Domain.Entidade;
using System;
using Xunit;

namespace DesafioAutoglass.Produtos.Tests
{
    public class ProdutoTeste
    {
        [Fact]
        public void Produto_Domain_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() =>
                new Produto(Guid.NewGuid(), string.Empty, 0, DateTime.Now, DateTime.Now.AddMonths(8))
            );

            //Assert
            Assert.Equal("O campo Descricao do produto não pode estar vazio", ex.Message);


            // Arrange & Act
            ex = Assert.Throws<DomainException>(() =>
                  new Produto(Guid.NewGuid(), "Geléia", 0, DateTime.Now.AddMonths(8), DateTime.Now)
            );

            //Assert
            Assert.Equal("Data de fabricação deve ser anterior à data de validade.", ex.Message);


            // Arrange & Act
            ex = Assert.Throws<DomainException>(() =>
                  new Produto(Guid.Empty, "Geléia", 0, DateTime.Now, DateTime.Now.AddMonths(8))
            );

            //Assert
            Assert.Equal("O campo FornecedorId do produto não pode estar vazio", ex.Message);
        }
    }
}

