using MultRH.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultRH.Domain.Entities
{
    public class Pagamento
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public int PlanoId { get; set; }
        [Required]
        public Plano? Plano { get; set; }
        public string? MercadoPagoPaymentId { get; set; }
        public StatusPagamento Status { get; set; } = StatusPagamento.Pendente;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
