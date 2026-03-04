using System.ComponentModel.DataAnnotations;

namespace VagasAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        // "Admin" ou "Candidato"
        [Required]
        public string Perfil { get; set; }
    }
}