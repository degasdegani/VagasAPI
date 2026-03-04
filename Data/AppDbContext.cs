using Microsoft.EntityFrameworkCore;
using VagasAPI.Models;

namespace VagasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Email único por usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Candidatura → Vaga
            modelBuilder.Entity<Candidatura>()
                .HasOne(c => c.Vaga)
                .WithMany(v => v.Candidaturas)
                .HasForeignKey(c => c.VagaId);

            // Candidatura → Candidato
            modelBuilder.Entity<Candidatura>()
                .HasOne(c => c.Candidato)
                .WithMany(ca => ca.Candidaturas)
                .HasForeignKey(c => c.CandidatoId);

            // Candidato → Usuario
            modelBuilder.Entity<Candidato>()
                .HasOne(c => c.Usuario)
                .WithOne()
                .HasForeignKey<Candidato>(c => c.UsuarioId);
        }
    }
}