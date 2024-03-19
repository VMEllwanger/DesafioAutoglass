using AutoMapper;
using DesafioAutoglass.Core.Interfaces;
using DesafioAutoglass.Produtos.API.Controllers;
using DesafioAutoglass.Produtos.API.ViewModels;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DesafioAutoglass.Produtos.Tests
{
    public class ProdutosControllerTests
    {
        private readonly Mock<IProdutosServicos> _produtosServicosMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _produtosServicosMock = new Mock<IProdutosServicos>();
            _mapperMock = new Mock<IMapper>();
            _notificadorMock = new Mock<INotificador>();

            _controller = new ProdutosController(_produtosServicosMock.Object, _mapperMock.Object, _notificadorMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "60585144000189");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutos(It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            _produtosServicosMock.Setup(p => p.ObterProdutos(It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }


        [Fact]
        public async Task ObterPorCodigo_DeveRetornarProdutoQuandoExistir()
        {
            // Arrange
            int codigo = 123;
            var produto = new Produto(Guid.NewGuid(), Guid.NewGuid(), codigo, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), null);
            _produtosServicosMock.Setup(p => p.ObterPorCodigo(codigo)).ReturnsAsync(produto);
            _mapperMock.Setup(m => m.Map<ProdutosResponse>(produto)).Returns(new ProdutosResponse());

            // Act
            var result = await _controller.ObterPorCodigo(codigo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtoResponse = Assert.IsType<ProdutosResponse>(okResult.Value);
            Assert.NotNull(produtoResponse);
        }

        [Fact]
        public async Task ObterPorCodigo_DeveRetornarNotFoundQuandoNaoExistirProduto()
        {
            // Arrange
            int codigo = 123;
            _produtosServicosMock.Setup(p => p.ObterPorCodigo(codigo)).ReturnsAsync((Produto)null);

            // Act
            var result = await _controller.ObterPorCodigo(codigo);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }


        [Fact]
        public async Task ObterPorId_DeveRetornarProdutoQuandoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();
            var produto = new Produto(id, Guid.NewGuid(), 123, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(8), null);
            _produtosServicosMock.Setup(p => p.ObterPorId(id)).ReturnsAsync(produto);
            _mapperMock.Setup(m => m.Map<ProdutosResponse>(produto)).Returns(new ProdutosResponse());

            // Act
            var result = await _controller.ObterPorId(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtoResponse = Assert.IsType<ProdutosResponse>(okResult.Value);
            Assert.NotNull(produtoResponse);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFoundQuandoNaoExistirProduto()
        {
            // Arrange
            var id = Guid.NewGuid();
            _produtosServicosMock.Setup(p => p.ObterPorId(id)).ReturnsAsync((Produto)null);

            // Act
            var result = await _controller.ObterPorId(id);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }


        [Fact]
        public async Task ObterProdutosPorDataDeValidade_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "60585144000189");
            var dataValidade = DateTime.Now.AddMonths(1);
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), fornecedor.Id, 1000, "Geléia", Situacao.ATIVO, DateTime.Now, dataValidade, fornecedor),
                new Produto(Guid.NewGuid(), fornecedor.Id, 1001, "Geléia", Situacao.ATIVO, DateTime.Now, dataValidade, fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutosPorDataDeValidade(dataValidade, It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterProdutosPorDataDeValidade(dataValidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorDataDeValidade_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            var dataValidade = DateTime.Now.AddMonths(1);
            _produtosServicosMock.Setup(p => p.ObterProdutosPorDataDeValidade(dataValidade, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosPorDataDeValidade(dataValidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosVencidos_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            _produtosServicosMock.Setup(p => p.ObterProdutosVencidos(DateTime.Now, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosVencidos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosNoPrazo_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            _produtosServicosMock.Setup(p => p.ObterProdutosNoPrazo(DateTime.Now, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosNoPrazo();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorSituacao_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", "60585144000189");
            var situacao = Situacao.ATIVO;
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutosPorSituacao(situacao, It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterProdutosPorSituacao(situacao);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorSituacao_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            var situacao = Situacao.ATIVO;
            _produtosServicosMock.Setup(p => p.ObterProdutosPorSituacao(situacao, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosPorSituacao(situacao);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorCodigoFornecedor_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var codigoFornecedor = 123;
            var fornecedor = new Fornecedor(codigoFornecedor, "Emporio10 ME", "60585144000189");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutosPorCodigoFornecedor(codigoFornecedor, It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterProdutosPorCodigoFornecedor(codigoFornecedor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorCodigoFornecedor_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            var codigoFornecedor = 123;
            _produtosServicosMock.Setup(p => p.ObterProdutosPorCodigoFornecedor(codigoFornecedor, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosPorCodigoFornecedor(codigoFornecedor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorCNPJ_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var cnpj = "12345678901234";
            var fornecedor = new Fornecedor(1030, "Emporio10 ME", cnpj);
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutosPorCNPJ(cnpj, It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterProdutosPorCNPJ(cnpj);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorCNPJ_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            var cnpj = "12345678901234";
            _produtosServicosMock.Setup(p => p.ObterProdutosPorCNPJ(cnpj, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosPorCNPJ(cnpj);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorDescricaoFornecedor_DeveRetornarOkResultComListaDeProdutosQuandoExistir()
        {
            // Arrange
            var descricaoFornecedor = "Emporio10 ME";
            var fornecedor = new Fornecedor(1030, descricaoFornecedor, "60585144000189");
            var produtos = new List<Produto>
            {
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1000, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor),
                new Produto(Guid.NewGuid(), Guid.NewGuid(), 1001, "Geléia", Situacao.ATIVO, DateTime.Now, DateTime.Now.AddMonths(1), fornecedor)
            };
            _produtosServicosMock.Setup(p => p.ObterProdutosPorDescricaoFornecedor(descricaoFornecedor, It.IsAny<int>())).ReturnsAsync(produtos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(produtos)).Returns(new List<ProdutosResponse> {
                new ProdutosResponse
                {
                    CNPJ = produtos[0].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[0].Fornecedor.Codigo,
                    Codigo = produtos[0].Codigo,
                    DataDeFabricacao = produtos[0].DataDeFabricacao,
                    DataDeValidade = produtos[0].DataDeValidade,
                    Descricao = produtos[0].Descricao,
                    DescricaoFornecedor = produtos[0].Fornecedor.Descricao,
                    Situacao = produtos[0].Situacao
                },
                new ProdutosResponse
                {
                    CNPJ = produtos[1].Fornecedor.CNPJ,
                    CodigoFornecedor = produtos[1].Fornecedor.Codigo,
                    Codigo = produtos[1].Codigo,
                    DataDeFabricacao = produtos[1].DataDeFabricacao,
                    DataDeValidade = produtos[1].DataDeValidade,
                    Descricao = produtos[1].Descricao,
                    DescricaoFornecedor = produtos[1].Fornecedor.Descricao,
                    Situacao = produtos[1].Situacao
                }
                });

            // Act
            var result = await _controller.ObterProdutosPorDescricaoFornecedor(descricaoFornecedor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.NotEmpty(produtosResponse);
        }

        [Fact]
        public async Task ObterProdutosPorDescricaoFornecedor_DeveRetornarOkResultComListaVaziaQuandoNaoExistirProdutos()
        {
            // Arrange
            var descricaoFornecedor = "Emporio10";
            _produtosServicosMock.Setup(p => p.ObterProdutosPorDescricaoFornecedor(descricaoFornecedor, It.IsAny<int>())).ReturnsAsync(new List<Produto>());
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosResponse>>(It.IsAny<IEnumerable<Produto>>())).Returns(new List<ProdutosResponse>());

            // Act
            var result = await _controller.ObterProdutosPorDescricaoFornecedor(descricaoFornecedor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var produtosResponse = Assert.IsAssignableFrom<IEnumerable<ProdutosResponse>>(okResult.Value);
            Assert.Empty(produtosResponse);
        }

        [Fact]
        public async Task RemoverPorCodigo_DeveRetornarNoContentQuandoProdutoForRemovido()
        {
            // Arrange
            _produtosServicosMock.Setup(p => p.RemoverPorCodigo(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoverPorCodigo(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoverPorCodigo_DeveRetornarNoContentQuandoProdutoForRemovidoComSucesso()
        {
            // Arrange
            int codigo = 123;
            _produtosServicosMock.Setup(p => p.RemoverPorCodigo(codigo)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemoverPorCodigo(codigo);

            // Assert
            _produtosServicosMock.Verify(p => p.RemoverPorCodigo(codigo), Times.Once);
            var okResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal((int)HttpStatusCode.NoContent, okResult.StatusCode);
        }

    }
}
