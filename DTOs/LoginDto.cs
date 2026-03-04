using System.ComponentModel.DataAnnotations;

namespace VagasAPI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
}