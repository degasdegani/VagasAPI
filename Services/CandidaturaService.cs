using VagasAPI.DTOs;
using VagasAPI.Models;
using VagasAPI.Repositories;

namespace VagasAPI.Services
{
    public class CandidaturaService : ICandidaturaService
    {
        private readonly ICandidaturaRepository _repository;
        private readonly ILogger<CandidaturaService> _logger;

        public CandidaturaService(ICandidaturaRepository repository, ILogger<CandidaturaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<CandidaturaResponseDto>> GetAllAsync()
        {
            var candidaturas = await _repository.GetAllAsync();
            return candidaturas.Select(c => new CandidaturaResponseDto
            {
                Id = c.Id,
                TituloVaga = c.Vaga?.Titulo ?? "",
                NomeCandidato = c.Candidato?.Usuario?.Nome ?? "",
                Status = c.Status,
                DataCandidatura = c.DataCandidatura
            }).ToList();
        }

        public async Task<CandidaturaResponseDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return null;
            return new CandidaturaResponseDto
            {
                Id = c.Id,
                TituloVaga = c.Vaga?.Titulo ?? "",
                NomeCandidato = c.Candidato?.Usuario?.Nome ?? "",
                Status = c.Status,
                DataCandidatura = c.DataCandidatura
            };
        }

        public async Task<CandidaturaResponseDto> CreateAsync(CandidaturaDto dto)
        {
            var candidatura = new Candidatura
            {
                VagaId = dto.VagaId,
                CandidatoId = dto.CandidatoId,
                Status = "Recebida",
                DataCandidatura = DateTime.Now
            };

            var criada = await _repository.CreateAsync(candidatura);

            _logger.LogInformation("Candidatura criada — Id: {Id}, VagaId: {VagaId}", criada.Id, criada.VagaId);

            // ✅ Busca completa com Vaga e Candidato carregados
            var completa = await _repository.GetByIdAsync(criada.Id);

            return new CandidaturaResponseDto
            {
                Id = completa!.Id,
                TituloVaga = completa.Vaga?.Titulo ?? "",
                NomeCandidato = completa.Candidato?.Usuario?.Nome ?? "",
                Status = completa.Status,
                DataCandidatura = completa.DataCandidatura
            };
        }

        public async Task<bool> AtualizarStatusAsync(int id, string novoStatus)
        {
            var statusValidos = new[] { "Recebida", "EmAnalise", "Entrevista", "Aprovado", "Reprovado" };

            if (!statusValidos.Contains(novoStatus))
                return false;

            var candidatura = await _repository.GetByIdAsync(id);
            if (candidatura == null) return false;

            await _repository.UpdateStatusAsync(candidatura, novoStatus);

            _logger.LogInformation("Status atualizado — CandidaturaId: {Id}, NovoStatus: {Status}", id, novoStatus);

            return true;
        }
    }
}