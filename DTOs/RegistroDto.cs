using System.ComponentModel.DataAnnotations;

namespace VagasAPI.DTOs
{
    public class RegistroDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        // "Admin" ou "Candidato"
        public string Perfil { get; set; } = "Candidato";
    }
}