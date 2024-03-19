using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Interfaces
{
    public interface IProdutosServicos
    {
        Task<IEnumerable<Produto>> ObterProdutos(int paginaAtual);
        Task<Produto> ObterPorCodigo(int Codigo);
        Task<Produto> ObterPorId(Guid Id);
        Task<IEnumerable<Produto>> ObterProdutosPorDescricao(string descricao, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosPorDataDeValidade(DateTime dataDeValidade, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosNoPrazo(DateTime dataAtual, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosVencidos(DateTime dataAtual, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosPorSituacao(Situacao situacao, int paginaAtual);

        Task<IEnumerable<Produto>> ObterProdutosPorCodigoFornecedor(int codigo, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosPorCNPJ(string cnpj, int paginaAtual);
        Task<IEnumerable<Produto>> ObterProdutosPorDescricaoFornecedor(string descricao, int paginaAtual);

        Task<Produto> Adicionar(Produto produto);
        Task<bool> Atualizar(int codigo, Produto produto);
        Task<bool> RemoverPorCodigo(int codigo);
    }
}
