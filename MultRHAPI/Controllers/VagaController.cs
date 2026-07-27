using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultRH.Application.Vagas;
using MultRH.Application.Vagas.Dtos;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly IVagaService _vagaService;
        private readonly IVagaPdfGenerator _pdfGenerator;

        public VagaController(IVagaService vagaService, IVagaPdfGenerator vagaPdfGenerator)
        {
            _vagaService = vagaService;
            _pdfGenerator = vagaPdfGenerator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _vagaService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var vaga = await _vagaService.GetById(id);
            return vaga is null ? NotFound() : Ok(vaga);
        }

        [HttpGet("{id}/pdf")]
        [Authorize(Policy = "VagaPdfAccess")]
        public async Task<IActionResult> GetPdf(int id)
        {
            var vaga = await _vagaService.GetById(id);
            if (vaga is null) return NotFound();

            var pdfBytes = _pdfGenerator.Generate(vaga);
            return File(pdfBytes, "application/pdf", $"encaminhamento-vaga-{id}.pdf");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateVagaDto dto)
        {
            var vaga = await _vagaService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = vaga.Id }, vaga);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateVagaDto dto)
        {
            var updated = await _vagaService.Update(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _vagaService.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
