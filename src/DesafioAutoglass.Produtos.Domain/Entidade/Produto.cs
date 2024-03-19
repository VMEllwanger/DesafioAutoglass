using DesafioAutoglass.Core.DomainDeObjetos;
using DesafioAutoglass.Produtos.Domain.Enums;
using System;

namespace DesafioAutoglass.Produtos.Domain.Entidade
{
    public class Produto : Entity, IAggregateRoot
    {
        public Guid FornecedorId { get; private set; }
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public Situacao Situacao { get; private set; }
        public DateTime DataDeFabricacao { get; private set; }
        public DateTime DataDeValidade { get; private set; }
        public Fornecedor Fornecedor { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Produto() { }

        public Produto(Guid id, Guid fornecedorId, int codigo, string descricao, Situacao situacao, DateTime dataDeFabricacao, DateTime dataDeValidade, Fornecedor fornecedor)
        {
            Id = id;
            FornecedorId = fornecedorId;
            Codigo = codigo;
            Descricao = descricao;
            Situacao = situacao;
            DataDeFabricacao = dataDeFabricacao;
            DataDeValidade = dataDeValidade;
            Fornecedor = fornecedor;
            Validar();
        }

        public Produto(Guid fornecedorId, string descricao, int codigo, DateTime dataDeFabricacao, DateTime dataDeValidade)
        {
            FornecedorId = fornecedorId;
            Descricao = descricao;
            Codigo = codigo;
            Situacao = Situacao.ATIVO;
            DataDeFabricacao = dataDeFabricacao;
            DataDeValidade = dataDeValidade;

            Validar();
        }

        public void Desativar() => Situacao = Situacao.INATIVO;

        public void ValidarDataFabricacaoEValidade()
        {
            if (DataDeFabricacao >= DataDeValidade)
            {
                throw new Exception("Data de fabricação deve ser anterior à data de validade.");
            }
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Descricao, "O campo Descricao do produto não pode estar vazio");
            Validacoes.ValidarDataFabricacaoEValidade(DataDeFabricacao, DataDeValidade,
                "Data de fabricação deve ser anterior à data de validade.");
            Validacoes.ValidarSeIgual(FornecedorId, Guid.Empty, "O campo FornecedorId do produto não pode estar vazio");
        }
    }
}