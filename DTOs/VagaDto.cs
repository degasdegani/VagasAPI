using System.ComponentModel.DataAnnotations;

namespace VagasAPI.DTOs
{
    // DTO para criar/atualizar vaga
    public class VagaDto
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Modalidade { get; set; }

        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }
    }

    // DTO para retornar vaga na resposta
    public class VagaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Area { get; set; }
        public string Cidade { get; set; }
        public string Modalidade { get; set; }
        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int TotalCandidaturas { get; set; }
    }
}