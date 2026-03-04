using System.ComponentModel.DataAnnotations;

namespace VagasAPI.Models
{
    public class Vaga
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(50)]
        public string Area { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cidade { get; set; }

        // "Remoto", "Presencial" ou "Híbrido"
        [Required]
        public string Modalidade { get; set; }

        [Range(0, 99999)]
        public decimal SalarioMinimo { get; set; }

        [Range(0, 99999)]
        public decimal SalarioMaximo { get; set; }

        public bool Ativa { get; set; } = true;

        public DateTime DataPublicacao { get; set; } = DateTime.Now;

        // Relacionamento — uma vaga tem muitas candidaturas
        public List<Candidatura> Candidaturas { get; set; } = new();
    }
}