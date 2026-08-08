
using System.ComponentModel.DataAnnotations;


namespace OndeFoi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrogatório.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string SenhaHash { get; set; } = string.Empty;
        public List<Categoria> Categorias { get; set; } = new();
        public List<Gasto> Gastos { get; set; } = new();
        public decimal Renda { get; set; }
    
    public Usuario() { }
    
    public Usuario(string nome, string email, string senhaHash)
        {
            this.Nome = nome;
            this.Email = email;
            this.SenhaHash = senhaHash;
        }
    }
}