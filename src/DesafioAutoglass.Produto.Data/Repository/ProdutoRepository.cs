using DesafioAutoglass.Core.Data;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Data.Repository
{
    internal class ProdutoRepository : IProdutorRepository
    {
        private readonly ProdutoContext _context;

        public ProdutoRepository(ProdutoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWorck => _context;

        public async Task<bool> AdicionarProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            return await _context.Commit();
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            return await _context.Commit();
        }

        public async Task<Produto> ObterPorCodigo(int codigo)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.CodigoProduto == codigo);
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodos(int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = query.Skip(indiceInicial).Take(itensPorPagina);

            return await Task.FromResult(produtosPaginados.ToList());

        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorDescricao(string descricao, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.Descricao.Contains(descricao));
            }

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorDataDeValidade(DateTime dataDeValidade, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.DataDeValidade >= dataDeValidade).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosVencidos(DateTime dataAtual, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.DataDeValidade < dataAtual).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosVencidosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosVencidosPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosNoPrazo(DateTime dataAtual, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.DataDeValidade >= dataAtual).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosNoPrazoPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosNoPrazoPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorSituacao(Situacao situacao, int paginaAtual, int itensPorPagina)
        {
            var situacaoInt = (int)situacao;

            var query = _context.Produtos.Where(p => (int)p.Situacao == situacaoInt).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosSituacaoPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosSituacaoPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorCodigoFornecedor(int codigo, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.Fornecedor.Codigo == codigo).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorCNPJ(string cnpj, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.Fornecedor.CNPJ == cnpj).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosPaginados;
        }

        public async Task<IEnumerable<Produto>> ObterProdutodPorDescricaoFornecedor(string descricao, int paginaAtual, int itensPorPagina)
        {
            var query = _context.Produtos.Where(p => p.Fornecedor.Descricao.Contains(descricao)).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * itensPorPagina;

            var produtosPaginados = await query.Skip(indiceInicial).Take(itensPorPagina).ToListAsync();

            return produtosPaginados;
        }

        public async Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            return await _context.Commit();
        }

        public async Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor)
        {
            _context.Entry(fornecedor).State = EntityState.Modified;
            return await _context.Commit();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
