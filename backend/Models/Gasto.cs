
using System.ComponentModel.DataAnnotations;


namespace OndeFoi.Models
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Gasto(string descricao)
        {
            Descricao = descricao;
        }

        public Gasto(string descricao, decimal valor, DateTime data, int categoriaId, int usuarioId)
        {
            this.Descricao = descricao;
            this.Valor = valor;
            this.Data = data;
            this.CategoriaId = categoriaId;
            this.UsuarioId = usuarioId;
        }
    }
}