using DesafioAutoglass.Produtos.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<IEnumerable<Fornecedor>> ObterFornecedores(int paginaAtual);
        Task<Fornecedor> ObterPorCodigo(int Codigo);
        Task<Fornecedor> ObterPorId(Guid Id);
    }
}
