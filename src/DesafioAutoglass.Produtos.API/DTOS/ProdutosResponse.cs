using DesafioAutoglass.Produtos.Domain.Enums;
using System;

namespace DesafioAutoglass.Produtos.API.ViewModels
{
    public class ProdutosResponse
    {
        public int CodigoFornecedor { get; set; }
        public string Descricao { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataDeFabricacao { get; set; }
        public DateTime DataDeValidade { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }
        public int Codigo { get; set; }
    }
}
