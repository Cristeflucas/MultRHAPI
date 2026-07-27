using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Vagas.Dtos
{
    public class CreateVagaDto
    {
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Empresa { get; set; }
        [Required]
        public string? Descricao { get; set; }
        [Required]
        public string? Endereco { get; set; }
        public string? PontoRef { get; set; }
        public DateOnly? DataEntrevista { get; set; }
        public TimeOnly? HorarioEntrevista { get; set; }
        public string? ResponsavelEntrevista { get; set; }
    }
}
