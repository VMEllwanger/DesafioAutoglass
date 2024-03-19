using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly ProdutoContext _context;
        const int ITEM_POR_PAGINA = 25;

        public FornecedorRepository(ProdutoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(int paginaAtual)
        {
            var query = _context.Fornecedores.AsQueryable();
            int indiceInicial = (paginaAtual - 1) * ITEM_POR_PAGINA;

            var produtosPaginados = query.Skip(indiceInicial).Take(ITEM_POR_PAGINA);

            return await Task.FromResult(produtosPaginados.ToList());
        }

        public async Task<Fornecedor> ObterPorCodigo(int codigo)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(f => f.Codigo == codigo);
        }

        public async Task<Fornecedor> ObterPorId(Guid Id)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}
