using DesafioAutoglass.Core.Data;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Data.Repository
{
    public class ProdutoRepository : IProdutosRepository
    {
        private readonly ProdutoContext _context;
        const int ITEM_POR_PAGINA = 25;

        public ProdutoRepository(ProdutoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWorck => _context;

        public async Task<Produto> AdicionarProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.Commit();
            return produto;
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            return await _context.Commit();
        }

        public async Task<Produto> ObterPorId(Guid Id)
        {
            return await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Produto> ObterPorCodigo(int codigo)
        {
            return await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<IEnumerable<Produto>> ObterTodos(int paginaAtual)
        {
            var query = _context.Produtos.Include(p => p.Fornecedor).AsQueryable();

            int indiceInicial = (paginaAtual - 1) * ITEM_POR_PAGINA;

            return await query.Skip(indiceInicial).Take(ITEM_POR_PAGINA).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorArgumento(Expression<Func<Produto, bool>> predicate, int paginaAtual)
        {
            var query = _context.Produtos.Where(predicate).AsQueryable().Include(p => p.Fornecedor);


            int indiceInicial = (paginaAtual - 1) * ITEM_POR_PAGINA;

            return await query.Skip(indiceInicial).Take(ITEM_POR_PAGINA).ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
