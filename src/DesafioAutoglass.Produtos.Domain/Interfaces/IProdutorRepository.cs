using DesafioAutoglass.Core.Data;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Interfaces
{
    public interface IProdutorRepository : IRepository<Produto>
    {
        Task<Produto> ObterPorCodigo(int codigoProduto);
        Task<IEnumerable<Produto>> ObterTodos(int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutodPorDescricao(string descricao, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutodPorDataDeValidade(DateTime dataDeValidade, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutosNoPrazo(DateTime dataAtual, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutosVencidos(DateTime dataAtual, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutodPorSituacao(Situacao situacao, int paginaAtual, int itensPorPagina);

        Task<IEnumerable<Produto>> ObterProdutodPorCodigoFornecedor(int codigo, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutodPorCNPJ(string cnpj, int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Produto>> ObterProdutodPorDescricaoFornecedor(string decricao, int paginaAtual, int itensPorPagina);

        Task<bool> AdicionarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(Produto produto);

        Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor);
        Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor);


    }
}
