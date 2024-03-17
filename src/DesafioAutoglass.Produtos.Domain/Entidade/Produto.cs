using DesafioAutoglass.Core.DomainDeObjetos;
using DesafioAutoglass.Produtos.Domain.Enums;
using System;

namespace DesafioAutoglass.Produtos.Domain.Entidade
{
    public class Produto : Entity, IAggregateRoot
    {
        public Guid FornecedorId { get; private set; }
        public int CodigoProduto { get; private set; }
        public string Descricao { get; private set; }
        public Situacao Situacao { get; private set; }
        public DateTime DataDeFabricacao { get; private set; }
        public DateTime DataDeValidade { get; private set; }
        public string CodigoFornecedor { get; private set; }
        public string DescricaoFornecedor { get; private set; }
        public string CNPJFornecedor { get; private set; }
        public Fornecedor Fornecedor { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Produto() { }
        public Produto(Guid fornecedorId, string descricao, DateTime dataDeFabricacao, DateTime dataDeValidade, string codigoFornecedor, string descricaoFornecedor, string cnpjFornecedor)
        {
            FornecedorId = fornecedorId;
            Descricao = descricao;
            Situacao = Situacao.ATIVO;
            DataDeFabricacao = dataDeFabricacao;
            DataDeValidade = dataDeValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cnpjFornecedor;
        }

        public void Desativar() => Situacao = Situacao.INATIVO;

        public void ValidarDataFabricacaoEValidade()
        {
            if (DataDeFabricacao >= DataDeValidade)
            {
                throw new Exception("Data de fabricação deve ser anterior à data de validade.");
            }
        }
    }


}