using VagasAPI.DTOs;

namespace VagasAPI.Services
{
    public interface ICandidaturaService
    {
        Task<List<CandidaturaResponseDto>> GetAllAsync();
        Task<CandidaturaResponseDto?> GetByIdAsync(int id);
        Task<CandidaturaResponseDto> CreateAsync(CandidaturaDto dto);
        Task<bool> AtualizarStatusAsync(int id, string novoStatus);
    }
}