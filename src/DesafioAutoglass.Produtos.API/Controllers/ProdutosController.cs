using AutoMapper;
using DesafioAutoglass.Core.Interfaces;
using DesafioAutoglass.Produtos.API.Models;
using DesafioAutoglass.Produtos.API.ViewModels;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using DevIO.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.API.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutosServicos _produtosService;
        private readonly IMapper _mapper;

        public ProdutosController(
                                    IProdutosServicos produtosService,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _produtosService = produtosService;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<ProdutosResponse>> ObterTodos([FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutos(pagina)).ToList();

            return Ok(result);
        }

        [HttpGet("ObterPorCodigo")]
        public async Task<ActionResult<ProdutosResponse>> ObterPorCodigo(int codigo)
        {
            var result = _mapper.Map<ProdutosResponse>(await _produtosService.ObterPorCodigo(codigo));

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<ProdutosResponse>> ObterPorId(Guid id)
        {
            var result = _mapper.Map<ProdutosResponse>(await _produtosService.ObterPorId(id));
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("ObterProdutosPorDataDeValidade")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosPorDataDeValidade([FromQuery] DateTime dataValidade, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosPorDataDeValidade(dataValidade, pagina)).ToList();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        [HttpGet("ProdutosVencidos")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosVencidos([FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosVencidos(DateTime.Now, pagina)).ToList();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        [HttpGet("ProdutosNoPrazo")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosNoPrazo([FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosNoPrazo(DateTime.Now, pagina));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("Situacao")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosPorSituacao([FromQuery] Situacao situacao, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosPorSituacao(situacao, pagina));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("CodigoFornecedor/{codigoFornecedor}")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosPorCodigoFornecedor([FromRoute] int codigoFornecedor, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosPorCodigoFornecedor(codigoFornecedor, pagina));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("CNPJ/{cnpj}")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosPorCNPJ([FromRoute] string cnpj, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosPorCNPJ(cnpj, pagina));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("DescricaoFornecedor")]
        public async Task<ActionResult<ProdutosResponse>> ObterProdutosPorDescricaoFornecedor([FromQuery] string descricaoFornecedor, [FromQuery] int pagina = 1)
        {
            var result = _mapper.Map<IEnumerable<ProdutosResponse>>(await _produtosService.ObterProdutosPorDescricaoFornecedor(descricaoFornecedor, pagina));
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutosResponse>> Adicionar(AdicionarProdutoModelRequest produtoViewModel)
        {
            var produto = await _produtosService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            var result = _mapper.Map<ProdutosResponse>(produto);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpPatch("{codigo:int}/Atualizar")]
        public async Task<IActionResult> AtualizarPorCodigoProduto([FromRoute] int codigo, [FromBody] AtualizarProdutoModelRequest produtoViewModel)
        {
            if (codigo != produtoViewModel.Codigo)
            {
                NotificarErro("O id informado não corresponde o mesmo que foi passado na query");
                return CustomResponse(HttpStatusCode.NoContent, produtoViewModel);
            }
            await _produtosService.Atualizar(codigo, _mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("Remover/{codigo}")]
        public async Task<IActionResult> RemoverPorCodigo([FromRoute] int codigo)
        {
            await _produtosService.RemoverPorCodigo(codigo);


            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
