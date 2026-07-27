using System.ComponentModel.DataAnnotations;

namespace MultRH.Domain.Entities
{
    public class Vaga
    {
        public int Id { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Empresa { get; set; }
        [Required]
        public string? Descricao { get; set; }
        public string? Endereco { get; set; }
        public string? PontoRef { get; set; }
        [Required]
        public DateOnly? DataEntrevista { get; set; }
        public TimeOnly? HorarioEntrevista { get; set; }
        public string? ResponsavelEntrevista { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
