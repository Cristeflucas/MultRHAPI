using MultRH.Domain.Entities;
using AutoMapper;
using MultRH.Application.Planos.Dtos;

namespace MultRH.Application.Planos.Profiles
{
    public class PlanoProfile : Profile
    {
        public PlanoProfile()
        {
            CreateMap<Plano, PlanoDto>();
            CreateMap<CreatePlanoDto, Plano>();
            CreateMap<UpdatePlanoDto, Plano>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
