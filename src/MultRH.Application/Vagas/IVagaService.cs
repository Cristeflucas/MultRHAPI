using MultRH.Application.Vagas.Dtos;

namespace MultRH.Application.Vagas
{
    public interface IVagaService
    {
        Task<List<VagaDto>> GetAll();
        Task<VagaDto?> GetById(int id);
        Task<VagaDto> Create(CreateVagaDto dto);
        Task<bool> Update(int id, UpdateVagaDto dto);
        Task<bool> Delete(int id);
    }
}
