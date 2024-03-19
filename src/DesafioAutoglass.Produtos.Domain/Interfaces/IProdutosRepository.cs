using DesafioAutoglass.Core.Data;
using DesafioAutoglass.Produtos.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Interfaces
{
    public interface IProdutosRepository : IRepository<Produto>
    {
        Task<Produto> ObterPorCodigo(int codigoProduto);
        Task<Produto> ObterPorId(Guid Id);
        Task<IEnumerable<Produto>> ObterTodos(int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosPorArgumento(Expression<Func<Produto, bool>> predicate, int paginaAtual);

        Task<Produto> AdicionarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(Produto produto);
    }
}
