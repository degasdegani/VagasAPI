using VagasAPI.DTOs;
using VagasAPI.Models;
using VagasAPI.Repositories;

namespace VagasAPI.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _repository;
        private readonly ILogger<VagaService> _logger;

        public VagaService(IVagaRepository repository, ILogger<VagaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PagedResultDto<VagaResponseDto>> GetAllAsync(string? area, string? modalidade, string? cidade, int pagina, int itensPorPagina)
        {
            _logger.LogInformation("Listando vagas — filtros: area={Area}, modalidade={Modalidade}, cidade={Cidade}", area, modalidade, cidade);

            var vagas = await _repository.GetAllAsync(area, modalidade, cidade);

            var total = vagas.Count;

            var dadosPaginados = vagas
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .Select(v => new VagaResponseDto
                {
                    Id = v.Id,
                    Titulo = v.Titulo,
                    Descricao = v.Descricao,
                    Area = v.Area,
                    Cidade = v.Cidade,
                    Modalidade = v.Modalidade,
                    SalarioMinimo = v.SalarioMinimo,
                    SalarioMaximo = v.SalarioMaximo,
                    Ativa = v.Ativa,
                    DataPublicacao = v.DataPublicacao,
                    TotalCandidaturas = v.Candidaturas?.Count ?? 0
                }).ToList();

            return new PagedResultDto<VagaResponseDto>
            {
                TotalItens = total,
                Pagina = pagina,
                ItensPorPagina = itensPorPagina,
                Dados = dadosPaginados
            };
        }

        public async Task<VagaResponseDto?> GetByIdAsync(int id)
        {
            var vaga = await _repository.GetByIdAsync(id);

            if (vaga == null) return null;

            return new VagaResponseDto
            {
                Id = vaga.Id,
                Titulo = vaga.Titulo,
                Descricao = vaga.Descricao,
                Area = vaga.Area,
                Cidade = vaga.Cidade,
                Modalidade = vaga.Modalidade,
                SalarioMinimo = vaga.SalarioMinimo,
                SalarioMaximo = vaga.SalarioMaximo,
                Ativa = vaga.Ativa,
                DataPublicacao = vaga.DataPublicacao,
                TotalCandidaturas = vaga.Candidaturas?.Count ?? 0
            };
        }

        public async Task<VagaResponseDto> CreateAsync(VagaDto dto)
        {
            var vaga = new Vaga
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Area = dto.Area,
                Cidade = dto.Cidade,
                Modalidade = dto.Modalidade,
                SalarioMinimo = dto.SalarioMinimo,
                SalarioMaximo = dto.SalarioMaximo,
                Ativa = true,
                DataPublicacao = DateTime.Now
            };

            var criada = await _repository.CreateAsync(vaga);

            _logger.LogInformation("Vaga criada — Id: {Id}, Titulo: {Titulo}", criada.Id, criada.Titulo);

            return new VagaResponseDto
            {
                Id = criada.Id,
                Titulo = criada.Titulo,
                Descricao = criada.Descricao,
                Area = criada.Area,
                Cidade = criada.Cidade,
                Modalidade = criada.Modalidade,
                SalarioMinimo = criada.SalarioMinimo,
                SalarioMaximo = criada.SalarioMaximo,
                Ativa = criada.Ativa,
                DataPublicacao = criada.DataPublicacao
            };
        }

        public async Task<bool> UpdateAsync(int id, VagaDto dto)
        {
            var vaga = await _repository.GetByIdAsync(id);

            if (vaga == null) return false;

            vaga.Titulo = dto.Titulo;
            vaga.Descricao = dto.Descricao;
            vaga.Area = dto.Area;
            vaga.Cidade = dto.Cidade;
            vaga.Modalidade = dto.Modalidade;
            vaga.SalarioMinimo = dto.SalarioMinimo;
            vaga.SalarioMaximo = dto.SalarioMaximo;

            await _repository.UpdateAsync(vaga);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vaga = await _repository.GetByIdAsync(id);

            if (vaga == null) return false;

            await _repository.DeleteAsync(vaga);
            return true;
        }
    }
}