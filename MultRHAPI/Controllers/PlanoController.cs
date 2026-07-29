using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultRH.Application.Planos;
using MultRH.Application.Planos.Dtos;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var planos = await _planoService.GetAll();
            return Ok(planos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var plano = await _planoService.GetById(id);
            if (plano is null) return NotFound();
            return Ok(plano);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreatePlanoDto dto)
        {
            var plano = await _planoService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = plano.Id }, plano);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdatePlanoDto dto)
        {
            var updated = await _planoService.Update(id, dto);
            return updated ? NoContent() : NotFound();
        }
    }
}
