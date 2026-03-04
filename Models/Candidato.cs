using System.ComponentModel.DataAnnotations;

namespace VagasAPI.Models
{
    public class Candidato
    {
        public int Id { get; set; }

        // FK para Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        [MaxLength(200)]
        public string LinkedIn { get; set; }

        // Relacionamento — um candidato tem muitas candidaturas
        public List<Candidatura> Candidaturas { get; set; } = new();
    }
}