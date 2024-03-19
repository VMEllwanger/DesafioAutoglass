using DesafioAutoglass.Core.Interfaces;
using DesafioAutoglass.Core.Servicos;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Servicos
{
    public class ProdutosServicos : BaseServicos, IProdutosServicos
    {
        private readonly IProdutosRepository _repository;
        private readonly IFornecedorRepository _repositoryFornecdor;

        public ProdutosServicos(IProdutosRepository repository, IFornecedorRepository repositoryFornecdor, INotificador notificador) : base(notificador)
        {
            _repository = repository;
            _repositoryFornecdor = repositoryFornecdor;
        }

        public Task<Produto> ObterPorId(Guid Id)
        {
            return _repository.ObterPorId(Id);
        }

        public Task<Produto> ObterPorCodigo(int Codigo)
        {
            return _repository.ObterPorCodigo(Codigo);
        }

        public async Task<IEnumerable<Produto>> ObterProdutos(int paginaAtual)
        {
            return await _repository.ObterTodos(paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorCNPJ(string cnpj, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.Fornecedor.CNPJ == cnpj, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorCodigoFornecedor(int codigo, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.Fornecedor.Codigo == codigo, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorDataDeValidade(DateTime dataDeValidade, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.DataDeValidade == dataDeValidade, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorDescricao(string descricao, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.Descricao.Contains(descricao), paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosPorDescricaoFornecedor(string descricao, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.Fornecedor.Descricao.Contains(descricao), paginaAtual);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorSituacao(Situacao situacao, int paginaAtual)
        {

            var retult = await _repository.ObterProdutosPorArgumento(p => p.Situacao == situacao, paginaAtual);
            return retult;
        }

        public Task<IEnumerable<Produto>> ObterProdutosNoPrazo(DateTime dataAtual, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.DataDeValidade >= dataAtual, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosVencidos(DateTime dataAtual, int paginaAtual)
        {
            return _repository.ObterProdutosPorArgumento(p => p.DataDeValidade < dataAtual, paginaAtual);
        }

        public async Task<bool> RemoverPorCodigo(int codigo)
        {
            var produto = await _repository.ObterPorCodigo(codigo);
            if (produto == null)
            {
                Notificar("Não existe produto com esse código");

                return false;
            }

            produto.Desativar();

            return await _repository.AtualizarProdutoAsync(produto);
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            if (produto == null)
            {
                Notificar("Erro ao retornar produto");
                return null;
            }

            var fornecedor = await _repositoryFornecdor.ObterPorCodigo(produto.Fornecedor.Codigo);

            if (fornecedor == null)
            {
                Notificar("Não existe fornecedor com esse código");
                return null;
            }

            var produtoASerSalvo = new Produto(fornecedor.Id, produto.Descricao, 0, produto.DataDeFabricacao, produto.DataDeValidade);

            return await _repository.AdicionarProdutoAsync(produtoASerSalvo);
        }

        public async Task<bool> Atualizar(int codigo, Produto produto)
        {
            var produtoRetornado = await _repository.ObterPorCodigo(codigo);


            if (produtoRetornado == null)
            {
                Notificar("Não existe produto com esse código");
                return false;
            }

            var fornecedor = await _repositoryFornecdor.ObterPorCodigo(produto.Fornecedor.Codigo);

            if (fornecedor == null)
            {
                Notificar("Não existe fornecedor com esse código");
                return false;
            }

            var produtoASerAtualizado = new Produto(produtoRetornado.Id, fornecedor.Id, produto.Codigo, produto.Descricao, produto.Situacao,
                produto.DataDeFabricacao, produto.DataDeValidade, fornecedor);


            return await _repository.AtualizarProdutoAsync(produtoASerAtualizado);
        }
    }
}
