using System.ComponentModel.DataAnnotations;

namespace OndeFoi.DTOs
{
    public class CadastrarUsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha precisa conter pelo menos 8 caracteres.")]
        public string Senha { get; set; } = string.Empty;
    }
}