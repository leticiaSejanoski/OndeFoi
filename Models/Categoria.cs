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

        public Categoria()
        {
        }

        public Categoria(string nome)
        {
            this.Nome = nome;
        }
        
    }
}