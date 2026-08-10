using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultRH.Application.Pagamentos;
using MultRH.Application.Pagamentos.Dtos;
using MultRH.Infrastructure.Pagamentos;
using System.Security.Claims;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly MercadoPagoWebhookValidator _webhookValidator;

        public PagamentoController(IPagamentoService pagamentoService, MercadoPagoWebhookValidator webhookValidator)
        {
            _pagamentoService = pagamentoService;
            _webhookValidator = webhookValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePagamentoDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var pagamento = await _pagamentoService.Create(userId, dto);
            return Ok(pagamento);
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook([FromQuery] string? id, [FromQuery(Name = "data.id")] string? dataId)
        {
            var resourceId = dataId ?? id;
            if (string.IsNullOrEmpty(resourceId)) return Ok();

            var xRequestId = Request.Headers["x-request-id"].ToString();
            var xSignature = Request.Headers["x-signature"].ToString();

            if (!_webhookValidator.IsValid(resourceId, xRequestId, xSignature))
            {
                return Unauthorized();
            }

            await _pagamentoService.ProcessarNotificacao(resourceId);
            return Ok();
        }
    }
}
