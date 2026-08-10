using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Assinaturas.Dtos
{
    public class CreateAssinaturaDto
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public int PlanoId { get; set; }
    }
}
