using System.ComponentModel.DataAnnotations;

namespace OndeFoi.DTOs
{
    public class EditarGastoDto
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria.")]
        public int CategoriaId { get; set; }
    }
}