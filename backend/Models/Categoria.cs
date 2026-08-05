using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OndeFoi.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrogatório.")]
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; }

        public Categoria() {}

        public Categoria(string nome, int usuarioId)
        {
            this.Nome = nome;
            this.UsuarioId = usuarioId;
        }
        
    }
}