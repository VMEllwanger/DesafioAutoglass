﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioAutoglass.Produtos.API.ViewModels
{
    public class FornecedorDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string? CNPJ { get; set; }
        public IEnumerable<ProdutosResponse>? Produtos { get; set; }
    }
}
