

using MultRH.Domain.Enums;

namespace MultRH.Application.Assinaturas.Dtos
{
    public class AssinaturaDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int PlanoId { get; set; }
        public string? PlanoNome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataExpiracao { get; set; }
        public StatusAssinatura Status { get; set; }
    }
}
