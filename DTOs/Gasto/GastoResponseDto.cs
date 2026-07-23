

namespace OndeFoi.DTOs
{
    public class GastoResponseDto
    {
        public int Id { get; set; }
        public string Descrição { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
    }
}