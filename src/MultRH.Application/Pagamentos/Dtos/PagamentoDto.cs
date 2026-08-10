using MultRH.Domain.Enums;

namespace MultRH.Application.Pagamentos.Dtos
{
    public class PagamentoDto
    {
        public int Id { get; set; }
        public StatusPagamento status { get; set; }
        public string? MercadoPagoPaymentId { get; set; }
    }
}
