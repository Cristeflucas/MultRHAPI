using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Pagamentos.Dtos
{
    public class CreatePagamentoPixDto
    {
        [Required]
        public int PlanoId { get; set; }
        [Required, EmailAddress]
        public string? PayerEmail { get; set; }
        [Required]
        public string? PayerCpf { get; set; }
    }
}
