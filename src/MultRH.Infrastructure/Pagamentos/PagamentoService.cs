using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using MultRH.Application.Assinaturas;
using MultRH.Application.Assinaturas.Dtos;
using MultRH.Application.Pagamentos;
using MultRH.Application.Pagamentos.Dtos;
using MultRH.Domain.Entities;
using MultRH.Domain.Enums;
using MultRH.Infrastructure.Data;

namespace MultRH.Infrastructure.Pagamentos
{
    public class PagamentoService : IPagamentoService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAssinaturaService _assinaturaService;

        public PagamentoService(UserDbContext context, IMapper mapper, IAssinaturaService assinaturaService)
        {
            _context = context;
            _mapper = mapper;
            _assinaturaService = assinaturaService;
        }

        public async Task<PagamentoDto> Create(string userId, CreatePagamentoDto dto)
        {
            var plano = await _context.Planos.FindAsync(dto.PlanoId) ?? throw new ApplicationException("Plano não encontrado");

            var pagamento = new Pagamento
            {
                UserId = userId,
                PlanoId = dto.PlanoId,
                Status = StatusPagamento.Pendente
            };
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            var request = new PaymentCreateRequest
            {
                TransactionAmount = plano.Valor,
                Token = dto.CardToken,
                Description = $"Assinatura do plano {plano.Nome} - Mult RH",
                Installments = 1,
                PaymentMethodId = dto.PaymentMethod,
                Payer = new PaymentPayerRequest { Email = dto.PayerEmail },
                ExternalReference = pagamento.Id.ToString()
            };
            var client = new PaymentClient();
            Payment resultado = await client.CreateAsync(request);

            pagamento.MercadoPagoPaymentId = resultado.Id.ToString();
            if (resultado.Status == "approved")
            {
                pagamento.Status = StatusPagamento.Aprovado;
                await _assinaturaService.Create(new CreateAssinaturaDto { UserId = userId, PlanoId = dto.PlanoId });
            }
            else if (resultado.Status == "rejected")
            {
                pagamento.Status = StatusPagamento.Rejeitado;
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<PagamentoDto>(pagamento);
        }

        public async Task ProcessarNotificacao(string mercadoPagoPaymentId)
        {
            var pagamento = await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.MercadoPagoPaymentId == mercadoPagoPaymentId);

            if (pagamento is null || pagamento.Status != StatusPagamento.Pendente)
            {
                return;
            }

            var client = new PaymentClient();
            Payment resultado = await client.GetAsync(long.Parse(mercadoPagoPaymentId));

            if (resultado.Status == "approved")
            {
                pagamento.Status = StatusPagamento.Aprovado;
                await _assinaturaService.Create(new CreateAssinaturaDto { UserId = pagamento.UserId, PlanoId = pagamento.PlanoId });
            }
            else if (resultado.Status == "rejected")
            {
                pagamento.Status = StatusPagamento.Rejeitado;
            }
            await _context.SaveChangesAsync();
        }
    }
}
