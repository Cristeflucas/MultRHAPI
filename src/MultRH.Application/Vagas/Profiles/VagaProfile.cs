using AutoMapper;
using MultRH.Application.Vagas.Dtos;
using MultRH.Domain.Entities;

namespace MultRH.Application.Vagas.Profiles
{
    public class VagaProfile : Profile
    {
        public VagaProfile()
        {
            CreateMap<Vaga, VagaDto>();
            CreateMap<CreateVagaDto, Vaga>();
            CreateMap<UpdateVagaDto, Vaga>();
        }
    }
}
