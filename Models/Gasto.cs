using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OndeFoi.Models
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Gasto() { }
        
        public Gasto(string descricao, decimal valor, int categoriaId, int usuarioId)
        {
            this.Descricao = descricao;
            this.Valor = valor;
            this.CategoriaId = categoriaId;
            this.UsuarioId = usuarioId;
        }
    }
}