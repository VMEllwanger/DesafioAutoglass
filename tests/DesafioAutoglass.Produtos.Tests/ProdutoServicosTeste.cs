using DesafioAutoglass.Core.Interfaces;
using DesafioAutoglass.Core.Notificacoes;
using DesafioAutoglass.Core.Servicos;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using DesafioAutoglass.Produtos.Domain.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DesafioAutoglass.Produtos.Tests
{
    public class ProdutoServicosTeste
    {
        private readonly ProdutosServicos _produtosServicos;
        private readonly Mock<IProdutosRepository> _repositoryProdutoMock;
        private readonly Mock<IFornecedorRepository> _repositoryFornecedorMock;
        private readonly Mock<INotificador> _notificadorMock;

        public ProdutoServicosTeste()
        {
            _repositoryProdutoMock = new Mock<IProdutosRepository>();
            _repositoryFornecedorMock = new Mock<IFornecedorRepository>();
            _notificadorMock = new Mock<INotificador>();
            var baseServicosMock = new Mock<BaseServicos>(_notificadorMock.Object);
            _produtosServicos = new ProdutosServicos(_repositoryProdutoMock.Object, _repositoryFornecedorMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarProdutoQuandoIdExistir()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var produto = new Produto(id, Guid.NewGuid(), 0, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), new Fornecedor(1020));
            _repositoryProdutoMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produto);

            // Act
            var resultado = await _produtosServicos.ObterPorId(id);

            // Assert
            Assert.Equal(produto, resultado);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNullQuandoIdNaoExistir()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _repositoryProdutoMock.Setup(r => r.ObterPorId(id)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _produtosServicos.ObterPorId(id);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterPorCodigo_DeveRetornarProdutoQuandoCodigoExistir()
        {
            // Arrange
            int codigo = 123;
            var produto = new Produto(Guid.NewGuid(), Guid.NewGuid(), codigo, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), new Fornecedor(1020));
            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync(produto);

            // Act
            var resultado = await _produtosServicos.ObterPorCodigo(codigo);

            // Assert
            Assert.Equal(produto, resultado);
        }
        [Fact]
        public async Task ObterPorCodigo_DeveRetornarNullQuandoCodigoNaoExistir()
        {
            // Arrange
            int codigo = 123;
            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _produtosServicos.ObterPorCodigo(codigo);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterProdutos_DeveRetornarProdutosDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), new Fornecedor(1020)),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), new Fornecedor(1020))
            };
            _repositoryProdutoMock.Setup(r => r.ObterTodos(paginaAtual)).ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutos(paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorCNPJ_DeveRetornarProdutosPorCNPJDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            string cnpj = "12345678901234";

            var fornecedor = new Fornecedor(1030, "Emporio10 ME", cnpj);
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };

            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorCNPJ(cnpj, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorCodigoFornecedor_DeveRetornarProdutosPorCodigoFornecedorDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            int codigoFornecedor = 1030;
            var fornecedor = new Fornecedor(codigoFornecedor, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };
            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorCodigoFornecedor(codigoFornecedor, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorDataDeValidade_DeveRetornarProdutosPorDataDeValidadeDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            DateTime dataDeValidade = new DateTime(2024, 12, 31);
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, dataDeValidade, fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, dataDeValidade, fornecedor)
            };
            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorDataDeValidade(dataDeValidade, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorDescricao_DeveRetornarProdutosPorDescricaoDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            string descricao = "Geléia";
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, descricao, Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, descricao, Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };
            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorDescricao(descricao, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorDescricaoFornecedor_DeveRetornarProdutosPorDescricaoFornecedorDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            string descricao = "Emporio10";
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };

            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorDescricaoFornecedor(descricao, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorSituacao_DeveRetornarProdutosPorSituacaoDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            Situacao situacao = Situacao.ATIVO;
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", situacao, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", situacao, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };

            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorSituacao(situacao, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosPorSituacao_DeveRetornarNullQuandoSituacaoNaoExistir()
        {
            // Arrange
            int paginaAtual = 1;
            Situacao situacao = Situacao.ATIVO;

            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync((IEnumerable<Produto>)null);

            // Act
            var resultado = await _produtosServicos.ObterProdutosPorSituacao(situacao, paginaAtual);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterProdutosNoPrazo_DeveRetornarProdutosNoPrazoDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            DateTime dataAtual = DateTime.Now;
            DateTime dataValidade = DateTime.Now.AddMonths(8);
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, dataValidade, fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, dataValidade, fornecedor)
            };

            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosNoPrazo(dataAtual, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task ObterProdutosVencidos_DeveRetornarProdutosVencidosDaPaginaAtual()
        {
            // Arrange
            int paginaAtual = 1;
            DateTime dataAtual = DateTime.Now;
            DateTime dataFabricacao = DateTime.Now.AddYears(-1);
            DateTime dataprodutosVencidos = DateTime.Now.AddMonths(-8);
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO,dataFabricacao, dataprodutosVencidos, fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO,dataFabricacao, dataprodutosVencidos, fornecedor)
            };
            _repositoryProdutoMock.Setup(r => r.ObterProdutosPorArgumento(
                    It.IsAny<Expression<Func<Produto, bool>>>(), paginaAtual))
                    .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtosServicos.ObterProdutosVencidos(dataprodutosVencidos, paginaAtual);

            // Assert
            Assert.Equal(produtos, resultado);
        }

        [Fact]
        public async Task RemoverPorCodigo_DeveRetornarFalseQuandoProdutoNaoExistir()
        {
            // Arrange
            int codigo = 123;

            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _produtosServicos.RemoverPorCodigo(codigo);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task RemoverPorCodigo_DeveDesativarProdutoQuandoExistir()
        {
            // Arrange
            int codigo = 123;
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), fornecedor.Id, 123, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);
            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync(produto);
            _repositoryProdutoMock.Setup(r => r.AtualizarProdutoAsync(produto)).ReturnsAsync(true);
            // Act
            var resultado = await _produtosServicos.RemoverPorCodigo(codigo);

            // Assert
            Assert.True(produto.Situacao == Situacao.INATIVO);
            Assert.True(resultado);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNullQuandoProdutoForNull()
        {
            // Arrange
            Produto produto = null;

            var mockFornecedorRepository = new Mock<IFornecedorRepository>();

            // Act
            var resultado = await _produtosServicos.Adicionar(produto);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNullQuandoFornecedorNaoExistir()
        {
            // Arrange 
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), Guid.NewGuid(), 123, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);
            _repositoryFornecedorMock.Setup(r => r.ObterPorCodigo(produto.Fornecedor.Codigo)).ReturnsAsync((Fornecedor)null);

            // Act
            var resultado = await _produtosServicos.Adicionar(produto);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Adicionar_DeveAdicionarProdutoQuandoFornecedorExistir()
        {
            // Arrange
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(IdFornecedor, 1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), IdFornecedor, 123, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);

            _repositoryFornecedorMock.Setup(r => r.ObterPorCodigo(produto.Fornecedor.Codigo)).ReturnsAsync(fornecedor);
            _repositoryProdutoMock.Setup(r => r.AdicionarProdutoAsync(It.IsAny<Produto>())).ReturnsAsync(produto);

            // Act
            var resultado = await _produtosServicos.Adicionar(produto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(fornecedor.Id, resultado.FornecedorId);
            Assert.Equal(produto.Descricao, resultado.Descricao);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalseQuandoProdutoNaoExistir()
        {
            // Arrange
            int codigo = 123;
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), Guid.NewGuid(), codigo, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);

            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(codigo)).ReturnsAsync((Produto)null);


            // Act
            var resultado = await _produtosServicos.Atualizar(codigo, produto);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalseQuandoNaoExistirProdutoComOCodigo()
        {
            // Arrange 
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(IdFornecedor, 1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), IdFornecedor, 1500, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);
            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _produtosServicos.Atualizar(123, produto);

            // Assert
            Assert.False(resultado);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFalseQuandoNaoExistirFornecedorComOCodigo()
        {
            // Arrange
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(IdFornecedor, 1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), IdFornecedor, 1500, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);

            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync(produto);
            _repositoryFornecedorMock.Setup(r => r.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync((Fornecedor)null);

            // Act
            var resultado = await _produtosServicos.Atualizar(123, produto);

            // Assert
            Assert.False(resultado);
            _notificadorMock.Verify(n => n.Handle(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarTrueQuandoProdutoEFornecedorExistem()
        {
            // Arrange
            var IdFornecedor = Guid.NewGuid();
            var fornecedor = new Fornecedor(IdFornecedor, 1030, "Emporio10 ME", "12345678901234");
            var produto = new Produto(Guid.NewGuid(), IdFornecedor, 1500, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor);

            _repositoryProdutoMock.Setup(r => r.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync(produto);
            _repositoryFornecedorMock.Setup(r => r.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync(fornecedor);
            _repositoryProdutoMock.Setup(r => r.AtualizarProdutoAsync(It.IsAny<Produto>())).ReturnsAsync(true);

            // Act
            var resultado = await _produtosServicos.Atualizar(123, produto);

            // Assert
            Assert.True(resultado);
        }
    }
}
