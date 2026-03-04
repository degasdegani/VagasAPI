using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagasAPI.DTOs;
using VagasAPI.Services;

namespace VagasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CandidaturasController : ControllerBase
    {
        private readonly ICandidaturaService _service;

        public CandidaturasController(ICandidaturaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as candidaturas — somente Admin
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAll()
        {
            var candidaturas = await _service.GetAllAsync();
            return Ok(candidaturas);
        }

        /// <summary>
        /// Busca candidatura pelo id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var candidatura = await _service.GetByIdAsync(id);

            if (candidatura == null)
                return NotFound("Candidatura não encontrada.");

            return Ok(candidatura);
        }

        /// <summary>
        /// Cria uma nova candidatura
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create(CandidaturaDto dto)
        {
            var candidatura = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = candidatura.Id }, candidatura);
        }

        /// <summary>
        /// Atualiza o status da candidatura — somente Admin
        /// </summary>
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AtualizarStatus(int id, AtualizarStatusDto dto)
        {
            var atualizado = await _service.AtualizarStatusAsync(id, dto.NovoStatus);

            if (!atualizado)
                return BadRequest("Status inválido ou candidatura não encontrada.");

            return NoContent();
        }
    }
}