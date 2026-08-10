using MultRH.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultRH.Domain.Entities
{
    public class Assinatura
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public int PlanoId { get; set; }
        [Required]
        public Plano? Plano { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataExpiracao { get; set; }
        [Required]
        public StatusAssinatura Status { get; set; }
    }
}
