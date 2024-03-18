using AutoMapper;
using DesafioAutoglass.Produtos.API.DTOS;
using DesafioAutoglass.Produtos.API.ViewModels;
using DesafioAutoglass.Produtos.Domain.Entidade;

namespace DesafioAutoglass.Produtos.API.Configuracoes
{
    public class AutomapperConfiguracoes : Profile
    {
        public AutomapperConfiguracoes()
        {
            CreateMap<Produto, ProdutoDTO>()
             .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CodigoProduto));
            CreateMap<ProdutoDTO, Produto>()
                .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<ProdutoAtualizarDTO, Produto>()
                       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => src.Codigo))
                       .ForMember(dest => dest.CodigoFornecedor, opt => opt.MapFrom(src => src.CodigoFornecedor))
                       .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                       .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Ativo))
                       .ForMember(dest => dest.DataDeFabricacao, opt => opt.MapFrom(src => src.DataDeFabricacao))
                       .ForMember(dest => dest.DataDeValidade, opt => opt.MapFrom(src => src.DataDeValidade))
                       .ForMember(dest => dest.DescricaoFornecedor, opt => opt.MapFrom(src => src.DescricaoFornecedor))
                       .ForMember(dest => dest.CNPJFornecedor, opt => opt.MapFrom(src => src.CNPJFornecedor))
                       .ForMember(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.FornecedorId));

        }
    }
}