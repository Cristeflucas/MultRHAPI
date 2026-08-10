using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Pagamentos.Dtos
{
    public class CreatePagamentoDto
    {
        [Required]
        public int PlanoId { get; set; }
        [Required]
        public string? CardToken { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
        [Required, EmailAddress]
        public string? PayerEmail { get; set; }
    }
}
