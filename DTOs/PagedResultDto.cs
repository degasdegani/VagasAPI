namespace VagasAPI.DTOs
{
    public class PagedResultDto<T>
    {
        public int TotalItens { get; set; }
        public int Pagina { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / ItensPorPagina);
        public List<T> Dados { get; set; }
    }
}