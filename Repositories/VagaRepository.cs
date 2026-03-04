using Microsoft.EntityFrameworkCore;
using VagasAPI.Data;
using VagasAPI.Models;

namespace VagasAPI.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly AppDbContext _context;

        public VagaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vaga>> GetAllAsync(string? area, string? modalidade, string? cidade)
        {
            var query = _context.Vagas
                .Include(v => v.Candidaturas)
                .Where(v => v.Ativa)
                .AsQueryable();

            if (!string.IsNullOrEmpty(area))
                query = query.Where(v => v.Area.Contains(area));

            if (!string.IsNullOrEmpty(modalidade))
                query = query.Where(v => v.Modalidade == modalidade);

            if (!string.IsNullOrEmpty(cidade))
                query = query.Where(v => v.Cidade.Contains(cidade));

            return await query.ToListAsync();
        }

        public async Task<Vaga?> GetByIdAsync(int id)
        {
            return await _context.Vagas
                .Include(v => v.Candidaturas)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vaga> CreateAsync(Vaga vaga)
        {
            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();
            return vaga;
        }

        public async Task UpdateAsync(Vaga vaga)
        {
            _context.Vagas.Update(vaga);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vaga vaga)
        {
            vaga.Ativa = false; // soft delete — não apaga, só desativa
            await _context.SaveChangesAsync();
        }
    }
}