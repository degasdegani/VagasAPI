namespace VagasAPI.DTOs
{
    // DTO para criar candidatura
    public class CandidaturaDto
    {
        public int VagaId { get; set; }
        public int CandidatoId { get; set; }
    }

    // DTO para retornar candidatura
    public class CandidaturaResponseDto
    {
        public int Id { get; set; }
        public string TituloVaga { get; set; }
        public string NomeCandidato { get; set; }
        public string Status { get; set; }
        public DateTime DataCandidatura { get; set; }
    }

    // DTO para atualizar status
    public class AtualizarStatusDto
    {
        public string NovoStatus { get; set; }
    }
}