using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagasAPI.DTOs;
using VagasAPI.Services;

namespace VagasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VagasController : ControllerBase
    {
        private readonly IVagaService _service;
        private readonly ILogger<VagasController> _logger;

        public VagasController(IVagaService service, ILogger<VagasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Lista vagas ativas com filtros e paginação
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] string? area,
            [FromQuery] string? modalidade,
            [FromQuery] string? cidade,
            [FromQuery] int pagina = 1,
            [FromQuery] int itensPorPagina = 10)
        {
            var resultado = await _service.GetAllAsync(area, modalidade, cidade, pagina, itensPorPagina);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca uma vaga pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var vaga = await _service.GetByIdAsync(id);

            if (vaga == null)
                return NotFound("Vaga não encontrada.");

            return Ok(vaga);
        }

        /// <summary>
        /// Cria uma nova vaga — somente Admin
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(VagaDto dto)
        {
            var vaga = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = vaga.Id }, vaga);
        }

        /// <summary>
        /// Atualiza uma vaga existente — somente Admin
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, VagaDto dto)
        {
            var atualizado = await _service.UpdateAsync(id, dto);

            if (!atualizado)
                return NotFound("Vaga não encontrada.");

            return NoContent();
        }

        /// <summary>
        /// Desativa uma vaga — somente Admin
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletado = await _service.DeleteAsync(id);

            if (!deletado)
                return NotFound("Vaga não encontrada.");

            return NoContent();
        }
    }
}