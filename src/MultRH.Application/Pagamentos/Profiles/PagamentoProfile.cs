using AutoMapper;
using MultRH.Application.Pagamentos.Dtos;
using MultRH.Domain.Entities;

namespace MultRH.Application.Pagamentos.Profiles
{
    public class PagamentoProfile : Profile
    {
        public PagamentoProfile()
        {
            CreateMap<Pagamento, PagamentoDto>();
        }
    }
}
