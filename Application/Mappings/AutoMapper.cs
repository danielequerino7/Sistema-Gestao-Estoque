using System.Linq;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{

    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(dest => dest.QuantidadeEmEstoque,
                    opt => opt.MapFrom(src => src.Estoques.Sum(e => e.Quantidade)))
                .ForMember(dest => dest.SetorId,
                    opt => opt.MapFrom(src => src.Estoques.Sum(e => e.SetorId)));

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
