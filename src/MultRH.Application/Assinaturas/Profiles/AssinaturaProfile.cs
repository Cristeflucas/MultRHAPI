using AutoMapper;
using MultRH.Application.Assinaturas.Dtos;
using MultRH.Domain.Entities;

namespace MultRH.Application.Assinaturas.Profiles
{
    public class AssinaturaProfile : Profile
    {
        public AssinaturaProfile()
        {
            CreateMap<Assinatura, AssinaturaDto>()
                .ForMember(dest => dest.PlanoNome, opt => opt.MapFrom(src => src.Plano != null ? src.Plano.Nome : null));
        }
    }
}
