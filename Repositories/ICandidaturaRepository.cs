using VagasAPI.Models;

namespace VagasAPI.Repositories
{
    public interface ICandidaturaRepository
    {
        Task<List<Candidatura>> GetAllAsync();
        Task<Candidatura?> GetByIdAsync(int id);
        Task<Candidatura> CreateAsync(Candidatura candidatura);
        Task UpdateStatusAsync(Candidatura candidatura, string novoStatus);
    }
}