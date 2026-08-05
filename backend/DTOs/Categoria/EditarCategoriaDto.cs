using System.ComponentModel.DataAnnotations;

namespace OndeFoi.DTOs
{
    public class EditarCategoriaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
    }
}