using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Servicos
{
    public class ProdutosServicos : IProdutosServicos
    {
        private readonly IProdutosRepository _repository;
        public ProdutosServicos(IProdutosRepository repository)
        {
            _repository = repository;
        }

        public Task<Produto> ObterPorId(Guid Id)
        {
            return _repository.ObterPorId(Id);
        }

        public Task<Produto> ObterPorCodigo(int Codigo)
        {
            return _repository.ObterPorCodigo(Codigo);
        }

        public Task<IEnumerable<Produto>> ObterProdutos(int paginaAtual)
        {
            return _repository.ObterTodos(paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorCNPJ(string cnpj, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.Fornecedor.CNPJ == cnpj, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorCodigoFornecedor(int codigo, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.Fornecedor.Codigo == codigo, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorDataDeValidade(DateTime dataDeValidade, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.DataDeValidade == dataDeValidade, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorDescricao(string descricao, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.Descricao.Contains(descricao), paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorDescricaoFornecedor(string descricao, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.Fornecedor.Descricao.Contains(descricao), paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutodPorSituacao(Situacao situacao, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => (int)p.Situacao == (int)situacao, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosNoPrazo(DateTime dataAtual, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.DataDeValidade >= dataAtual, paginaAtual);
        }

        public Task<IEnumerable<Produto>> ObterProdutosVencidos(DateTime dataAtual, int paginaAtual)
        {
            return _repository.ObterProdutodPorArgumento(p => p.DataDeValidade < dataAtual, paginaAtual);
        }

        public async Task<bool> RemoverPorCodigo(int codigo)
        {
            var produto = await _repository.ObterPorCodigo(codigo);
            if (produto == null)
            {
                //Não existe produto com esse código
                return false;
            }

            produto.Desativar();

            return await _repository.AtualizarProdutoAsync(produto);
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            if (produto == null) return null;

            return await _repository.AdicionarProdutoAsync(produto);
        }

        public async Task<bool> Atualizar(int codigo, Produto produto)
        {
            var produtoRetornado = await _repository.ObterPorCodigo(codigo);

            if (produtoRetornado == null) return false;

            return await _repository.AtualizarProdutoAsync(produto);
        }
    }
}
