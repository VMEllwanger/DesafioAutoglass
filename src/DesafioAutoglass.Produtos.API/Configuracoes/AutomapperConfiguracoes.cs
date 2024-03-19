using AutoMapper;
using DesafioAutoglass.Produtos.API.Models;
using DesafioAutoglass.Produtos.API.ViewModels;
using DesafioAutoglass.Produtos.Domain.Entidade;
using DesafioAutoglass.Produtos.Domain.Enums;

namespace DesafioAutoglass.Produtos.API.Configuracoes
{
    public class AutomapperConfiguracoes : Profile
    {
        private readonly IMapper _mapper;

        public AutomapperConfiguracoes()
        {
            CreateMap<Produto, ProdutosResponse>()
                    .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
                    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                    .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao))
                    .ForMember(dest => dest.CodigoFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Codigo))
                    .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.Fornecedor.CNPJ))
                    .ForMember(dest => dest.DescricaoFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Descricao))
                    .ForMember(dest => dest.DataDeFabricacao, opt => opt.MapFrom(src => src.DataDeFabricacao))
                    .ForMember(dest => dest.DataDeValidade, opt => opt.MapFrom(src => src.DataDeValidade));

            CreateMap<ProdutosResponse, Produto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<AdicionarProdutoModelRequest, Produto>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.DataDeFabricacao, opt => opt.MapFrom(src => src.DataDeFabricacao))
                .ForMember(dest => dest.DataDeValidade, opt => opt.MapFrom(src => src.DataDeValidade))
                  .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => new Fornecedor(src.CodigoFornecedor)));

            CreateMap<AtualizarProdutoModelRequest, Produto>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.DataDeFabricacao, opt => opt.MapFrom(src => src.DataDeFabricacao))
                .ForMember(dest => dest.DataDeValidade, opt => opt.MapFrom(src => src.DataDeValidade))
                .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => new Fornecedor(src.CodigoFornecedor)));



            CreateMap<bool, Situacao>().ConvertUsing(b => b ? Situacao.ATIVO : Situacao.INATIVO);
            CreateMap<Situacao, bool>().ConvertUsing(s => s == Situacao.ATIVO);
        }
    }
}