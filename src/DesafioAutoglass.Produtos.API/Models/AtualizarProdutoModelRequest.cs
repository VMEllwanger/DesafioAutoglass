using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Produtos.API.Models
{
    public class AtualizarProdutoModelRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CodigoFornecedor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataDeFabricacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataDeValidade { get; set; }
    }
}
