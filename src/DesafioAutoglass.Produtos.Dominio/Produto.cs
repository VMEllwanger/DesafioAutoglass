using DesafioAutoglass.Core.DominioDeObjetos;
using System;

namespace DesafioAutoglass.Produtos.Dominio
{
    public class Produto : Entity, IAggregateRoot
    {
        public int CodigoProduto { get; private set; }
        public string Descricao { get; private set; }
        public Situacao Situacao { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public string CodigoFornecedor { get; private set; }
        public string DescricaoFornecedor { get; private set; }
        public string CNPJFornecedor { get; private set; }

        public Produto(string descricao, DateTime dataFabricacao, string codigoFornecedor, string descricaoFornecedor, string cnpjFornecedor)
        {
            Descricao = descricao;
            Situacao = Situacao.ATIVO;
            DataFabricacao = dataFabricacao;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cnpjFornecedor;
        }

        public void Desativar() => Situacao = Situacao.INATIVO;

    }

    public enum Situacao
    {
        INATIVO = 0,
        ATIVO = 1,
    }
}