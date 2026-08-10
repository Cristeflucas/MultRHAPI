using MultRH.Application.Assinaturas.Dtos;

namespace MultRH.Application.Assinaturas
{
    public interface IAssinaturaService
    {
        Task<AssinaturaDto> Create(CreateAssinaturaDto dto);
        Task<bool> Cancelar(int id);
        Task<AssinaturaDto?> GetAtivaPorUsuario(string userId);
        Task<bool> TemAssinaturaAtiva(string userId);
    }
}
