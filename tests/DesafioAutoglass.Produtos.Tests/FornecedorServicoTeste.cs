using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using DesafioAutoglass.Produtos.Domain.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DesafioAutoglass.Produtos.Tests
{
    public class FornecedorServicoTeste
    {
        private readonly FornecedorServico _fornecedorServico;
        private readonly Mock<IFornecedorRepository> _repositoryMock;

        public FornecedorServicoTeste()
        {
            _repositoryMock = new Mock<IFornecedorRepository>();
            _fornecedorServico = new FornecedorServico(_repositoryMock.Object);
        }

        [Fact]
        public async Task ObterFornecedores_DeveRetornarFornecedoresDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor("Emporio10 ME", "60585144000189") { },
                new Fornecedor("Pães e Doces Ltda", "69223161000140") { },
                new Fornecedor("Massa e Borda Pizzaria ME", "49526608000143") { }
            };
            _repositoryMock.Setup(r => r.ObterFornecedores(paginaAtual)).ReturnsAsync(fornecedores);

            // Act
            var resultado = await _fornecedorServico.ObterFornecedores(paginaAtual);

            // Assert
            Assert.Equal(fornecedores, resultado);
        }

        [Fact]
        public async Task ObterPorCodigo_DeveRetornarFornecedorQuandoCodigoExistir()
        {
            // Arrange
            int codigo = 1030;
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(Guid.NewGuid(), codigo, "Emporio10 ME", "12345678901234");
            _repositoryMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync(fornecedor);

            // Act
            var resultado = await _fornecedorServico.ObterPorCodigo(codigo);

            // Assert
            Assert.Equal(fornecedor, resultado);
        }

        [Fact]
        public async Task ObterPorCodigo_DeveRetornarNullQuandoCodigoNaoExistir()
        {
            // Arrange 
            int codigo = 1030;
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(Guid.NewGuid(), codigo, "Emporio10 ME", "12345678901234");
            _repositoryMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync((Fornecedor)null);

            // Act
            var resultado = await _fornecedorServico.ObterPorCodigo(codigo);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarFornecedorQuandoIdExistir()
        {
            // Arrange 
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(Guid.NewGuid(), 1030, "Emporio10 ME", "12345678901234");
            _repositoryMock.Setup(r => r.ObterPorId(IdFornecedor)).ReturnsAsync(fornecedor);

            // Act
            var resultado = await _fornecedorServico.ObterPorId(IdFornecedor);

            // Assert
            Assert.Equal(fornecedor, resultado);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoIdNaoExistir()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync((Fornecedor)null);

            // Act
            var resultado = await _fornecedorServico.ObterPorId(id);

            // Assert
            Assert.Null(resultado);
        }
    }
}
