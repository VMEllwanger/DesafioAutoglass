using System;
using System.Collections.Generic;

namespace DesafioAutoglass.Produtos.Domain.Entidade
{
    public class Fornecedor : Entity
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public ICollection<Produto> Produtos { get; set; }


        public Fornecedor(int codigo, string descricao, string cnpj)
        {
            Codigo = codigo;
            Descricao = descricao;
            CNPJ = cnpj;
        }

        public Fornecedor(string descricao, string cnpj)
        {
            Descricao = descricao;
            CNPJ = cnpj;
        }

        public Fornecedor(int codigo)
        {
            Codigo = codigo;
        }

        public Fornecedor(Guid id, int codigo, string cNPJ, string descricao) : this(codigo, descricao, cNPJ)
        {
            Id = id;
        }
    }
}
