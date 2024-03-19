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
            Assert.Equal("O campo Descricao do produto n�o pode estar vazio", ex.Message);


            // Arrange & Act
            ex = Assert.Throws<DomainException>(() =>
                  new Produto(Guid.NewGuid(), "Gel�ia", 0, DateTime.Now.AddMonths(8), DateTime.Now)
            );

            //Assert
            Assert.Equal("Data de fabrica��o deve ser anterior � data de validade.", ex.Message);


            // Arrange & Act
            ex = Assert.Throws<DomainException>(() =>
                  new Produto(Guid.Empty, "Gel�ia", 0, DateTime.Now, DateTime.Now.AddMonths(8))
            );

            //Assert
            Assert.Equal("O campo FornecedorId do produto n�o pode estar vazio", ex.Message);
        }
    }
}

