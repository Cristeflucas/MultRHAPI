using MultRH.Application.Planos.Dtos;

namespace MultRH.Application.Planos
{
    public interface IPlanoService
    {
        Task<List<PlanoDto>> GetAll();
        Task<PlanoDto?> GetById(int id);
        Task<PlanoDto> Create(CreatePlanoDto dto);
        Task<bool> Update(int id, UpdatePlanoDto dto);


    }
}
