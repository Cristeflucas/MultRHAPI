using MultRH.Application.Vagas.Dtos;

namespace MultRH.Application.Vagas
{
    public interface IVagaPdfGenerator
    {
        byte[] Generate(VagaDto vaga);
    }
}
