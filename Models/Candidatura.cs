using System.ComponentModel.DataAnnotations;

namespace VagasAPI.Models
{
    public class Candidatura
    {
        public int Id { get; set; }

        // FK para Vaga
        public int VagaId { get; set; }
        public Vaga Vaga { get; set; }

        // FK para Candidato
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }

        // "Recebida", "EmAnalise", "Entrevista", "Aprovado", "Reprovado"
        public string Status { get; set; } = "Recebida";

        public DateTime DataCandidatura { get; set; } = DateTime.Now;
    }
}