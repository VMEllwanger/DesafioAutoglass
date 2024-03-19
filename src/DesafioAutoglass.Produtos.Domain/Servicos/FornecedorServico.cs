using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.Domain.Servicos
{
    public class FornecedorServico : IFornecedorServico
    {
        private readonly IFornecedorRepository _repository;
        public FornecedorServico(IFornecedorRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(int paginaAtual)
        {
            return await _repository.ObterFornecedores(paginaAtual);
        }

        public async Task<Fornecedor> ObterPorCodigo(int codigo)
        {
            return await _repository.ObterPorCodigo(codigo);
        }

        public async Task<Fornecedor> ObterPorId(Guid Id)
        {
            return await _repository.ObterPorId(Id);
        }
    }
}
