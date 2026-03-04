using Microsoft.EntityFrameworkCore;
using VagasAPI.Data;
using VagasAPI.Models;

namespace VagasAPI.Repositories
{
    public class CandidaturaRepository : ICandidaturaRepository
    {
        private readonly AppDbContext _context;

        public CandidaturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Candidatura>> GetAllAsync()
        {
            return await _context.Candidaturas
                .Include(c => c.Vaga)
                .Include(c => c.Candidato)
                    .ThenInclude(ca => ca.Usuario)
                .ToListAsync();
        }

        public async Task<Candidatura?> GetByIdAsync(int id)
        {
            return await _context.Candidaturas
                .Include(c => c.Vaga)
                .Include(c => c.Candidato)
                    .ThenInclude(ca => ca.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidatura> CreateAsync(Candidatura candidatura)
        {
            _context.Candidaturas.Add(candidatura);
            await _context.SaveChangesAsync();
            return candidatura;
        }

        public async Task UpdateStatusAsync(Candidatura candidatura, string novoStatus)
        {
            candidatura.Status = novoStatus;
            await _context.SaveChangesAsync();
        }
    }
}