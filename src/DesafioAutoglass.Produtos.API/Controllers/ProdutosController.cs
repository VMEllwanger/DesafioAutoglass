using AutoMapper;
using DesafioAutoglass.Produtos.API.DTOS;
using DesafioAutoglass.Produtos.API.ViewModels;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.API.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosServicos _produtosService;
        private readonly IMapper _mapper;

        public ProdutosController(
                            IProdutosServicos produtosService,
                            IMapper mapper) : base()
        {
            _produtosService = produtosService;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<ProdutoDTO>> ObterTodos([FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutos(pagina)).ToList();

            return Ok(result);
        }

        [HttpGet("ObterPorCodigo")]
        public async Task<ActionResult<ProdutoDTO>> ObterPorCodigo(int codigo)
        {
            var result = _mapper.Map<ProdutoDTO>(await _produtosService.ObterPorCodigo(codigo));

            return Ok(result);
        }

        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<ProdutoDTO>> ObterPorId(Guid id)
        {
            var result = _mapper.Map<ProdutoDTO>(await _produtosService.ObterPorId(id));

            return Ok(result);
        }

        [HttpGet("ObterProdutodPorDataDeValidade")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutodPorDataDeValidade([FromQuery] DateTime dataValidade, [FromQuery] int pagina = 1)
        {
            var t = "ssa";
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutodPorDataDeValidade(dataValidade, pagina));

            return Ok(result);
        }


        [HttpGet("ProdutosVencidos")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutosVencidos([FromQuery] int pagina = 1)
        {
            var t = "ssa";
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutosVencidos(DateTime.Now, pagina));

            return Ok(result);
        }


        [HttpGet("ProdutosNoPrazo")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutosNoPrazo([FromQuery] int pagina = 1)
        {
            var t = "ssa";
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutosNoPrazo(DateTime.Now, pagina));

            return Ok(result);
        }

        [HttpGet("Situacao")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutodPorSituacao([FromQuery] Situacao situacao, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutodPorSituacao(situacao, pagina));

            return Ok(result);
        }

        [HttpGet("CodigoFornecedor/{codigoFornecedor}")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutodPorCodigoFornecedor([FromRoute] int codigoFornecedor, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutodPorCodigoFornecedor(codigoFornecedor, pagina));

            return Ok(result);
        }

        [HttpGet("CNPJ/{cnpj}")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutodPorCNPJ([FromRoute] string cnpj, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutodPorCNPJ(cnpj, pagina));

            return Ok(result);
        }

        [HttpGet("DescricaoFornecedor")]
        public async Task<ActionResult<ProdutoDTO>> ObterProdutodPorDescricaoFornecedor([FromQuery] string descricaoFornecedor, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtosService.ObterProdutodPorDescricaoFornecedor(descricaoFornecedor, pagina));

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Adicionar(ProdutoDTO produtoViewModel)
        {
            var produto = await _produtosService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            var result = _mapper.Map<ProdutoDTO>(produto);
            return Created("", result);
        }

        [HttpPatch("{codigo:int}/Atualizar")]
        public async Task<IActionResult> AtualizarPorCodigoProduto([FromRoute] int codigo, [FromBody] ProdutoAtualizarDTO produtoViewModel)
        {
            if (codigo != produtoViewModel.Codigo)
            {
                return BadRequest(new
                {
                    errors = "Os códigos não são correspondentes",

                });
            }
            await _produtosService.Atualizar(codigo, _mapper.Map<Produto>(produtoViewModel));

            return NoContent();
        }

        [HttpDelete("Remover/{codigo}")]
        public async Task<IActionResult> RemoverPorCodigo([FromRoute] int codigo)
        {
            await _produtosService.RemoverPorCodigo(codigo);

            return NoContent();
        }
    }
}
