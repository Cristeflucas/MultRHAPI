using MultRH.Application.Pagamentos.Dtos;

namespace MultRH.Application.Pagamentos
{
    public interface IPagamentoService 
    {
        Task<PagamentoDto> Create(string userId, CreatePagamentoDto dto);
        Task ProcessarNotificacao(string mercadoPagoPaymentId);
        Task<PagamentoPixDto> CreatePix(string userId, CreatePagamentoPixDto dto);

    }
}
