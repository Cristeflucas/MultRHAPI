using MultRH.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultRH.Domain.Entities
{
    public class Plano
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public Periodicidade Periodicidade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
