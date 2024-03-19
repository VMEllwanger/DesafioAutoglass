using AutoMapper;
using DesafioAutoglass.Produtos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoglass.Produtos.API.Controllers
{
    [Route("api/fornecedor")]
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorServico _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(
                            IFornecedorServico fornecedorService,
                            IMapper mapper) : base()
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult> ObterTodos([FromQuery] int pagina = 1)
        {
            var result = await _fornecedorService.ObterFornecedores(pagina);

            return Ok(result.ToList());
        }
    }
}
