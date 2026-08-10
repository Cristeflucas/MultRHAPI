using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultRH.Application.Assinaturas;
using MultRH.Application.Assinaturas.Dtos;
using System.Security.Claims;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssinaturaController : ControllerBase
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturaController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateAssinaturaDto dto)
        {
            var assinatura = await _assinaturaService.Create(dto);
            return Ok(assinatura);
        }

        [HttpPatch("{id}/cancelar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var cancelada = await _assinaturaService.Cancelar(id);
            return cancelada ? NoContent() : NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMinhaAssinatura()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var assinatura = await _assinaturaService.GetAtivaPorUsuario(userId);
            return assinatura is null ? NotFound() : Ok(assinatura);
        }
    }
}
