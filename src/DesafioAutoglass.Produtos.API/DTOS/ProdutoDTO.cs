using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Produtos.API.ViewModels
{
    public class ProdutoDTO
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CodigoFornecedor { get; private set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }


        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataDeFabricacao
        {
            get { return DataDeFabricacao; }
            private set
            {
                if (value >= DataDeValidade)
                {
                    throw new ArgumentException("A data de fabricação não pode ser maior ou igual à data de validade.");
                }

                DataDeFabricacao = value;
            }
        }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataDeValidade
        {
            get { return DataDeValidade; }
            private set
            {
                if (value <= DataDeFabricacao)
                {
                    throw new ArgumentException("A data de validade não pode ser menor ou igual à data de fabricação.");
                }

                DataDeValidade = value;
            }
        }

        public string DescricaoFornecedor { get; private set; }
        public string CNPJFornecedor { get; private set; }
        public int Codigo { get; private set; }
        public Guid FornecedorId { get; set; }
    }
}
