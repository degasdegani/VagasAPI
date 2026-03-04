using VagasAPI.Models;

namespace VagasAPI.Repositories
{
    public interface IVagaRepository
    {
        Task<List<Vaga>> GetAllAsync(string? area, string? modalidade, string? cidade);
        Task<Vaga?> GetByIdAsync(int id);
        Task<Vaga> CreateAsync(Vaga vaga);
        Task UpdateAsync(Vaga vaga);
        Task DeleteAsync(Vaga vaga);
    }
}