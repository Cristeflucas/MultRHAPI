using AutoMapper;
using MultRH.Application.Vagas.Dtos;
using MultRH.Application.Vagas.Profiles;
using MultRH.Domain.Entities;
using Xunit;


namespace MultRH.Application.Tests
{
    public class VagaProfileTests
    {
        private readonly IMapper _mapper;

        public VagaProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<VagaProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_CreateVagaDtoParaVaga()
        {
            var dto = new CreateVagaDto
            {
                Titulo = "ASG / Auxiliar de Limpeza",
                Empresa = "Fernandes Serviços",
                Descricao = "Limpeza geral",
                Endereco = "Rua Deputado Clóvis Motta, 5356",
                PontoRef = "Perto do Arena das Dunas",
                DataEntrevista = new DateOnly(2026, 7, 28),
                HorarioEntrevista = new TimeOnly(14, 0),
                ResponsavelEntrevista = "Srº. Alzamir"
            };
            var vaga = _mapper.Map<Vaga>(dto);

            Assert.Equal(dto.Titulo, vaga.Titulo);
            Assert.Equal(dto.Empresa, vaga.Empresa);
            Assert.Equal(dto.Descricao, vaga.Descricao);
            Assert.Equal(dto.Endereco, vaga.Endereco);
            Assert.Equal(dto.PontoRef, vaga.PontoRef);
            Assert.Equal(dto.DataEntrevista, vaga.DataEntrevista);
            Assert.Equal(dto.HorarioEntrevista, vaga.HorarioEntrevista);
            Assert.Equal(dto.ResponsavelEntrevista, vaga.ResponsavelEntrevista);
        }
    }
}
