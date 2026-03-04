using VagasAPI.DTOs;

namespace VagasAPI.Services
{
    public interface IVagaService
    {
        Task<PagedResultDto<VagaResponseDto>> GetAllAsync(string? area, string? modalidade, string? cidade, int pagina, int itensPorPagina);
        Task<VagaResponseDto?> GetByIdAsync(int id);
        Task<VagaResponseDto> CreateAsync(VagaDto dto);
        Task<bool> UpdateAsync(int id, VagaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}