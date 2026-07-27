using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Vagas.Dtos
{
    public class UpdateVagaDto
    {
        public string? Titulo { get; set; }
        public string? Empresa { get; set; }
        public string? Descricao { get; set; }
        public string? Endereco { get; set; }
        public string? PontoRef { get; set; }
        public DateOnly? DataEntrevista { get; set; }
        public TimeOnly? HorarioEntrevista { get; set; }
        public string? ResponsavelEntrevista { get; set; }
    }
}
