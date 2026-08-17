using MultRH.Domain.Enums;

namespace MultRH.Application.Pagamentos.Dtos
{
    public class PagamentoPixDto
    {
        public int Id { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public string? MercadoPagoPaymentId { get; set; }
        public string? QrCode { get; set; }
        public string? QrCodeBase64 { get; set; }
    }
}
