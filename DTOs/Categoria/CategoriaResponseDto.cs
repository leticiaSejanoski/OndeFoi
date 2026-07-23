using System.ComponentModel.DataAnnotations;

namespace OndeFoi.DTOs
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}