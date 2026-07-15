using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OndeFoi.Data;
using OndeFoi.Models;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public GastosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var gastos = _context.Gasto
            .Include(g => g.Categoria )
            .Select(g => new
            {
                g.Descricao,
                g.Valor,
                Categoria = g.Categoria.Nome
            })
            .OrderBy(g => g.Descricao)
            .ToList();

            return Ok(gastos);
        }

        [HttpPost]
        public IActionResult Criar(string descricao, decimal valor, int idCategoria, int idUsuario)
        {
            Gasto gasto = new Gasto(descricao, valor, idCategoria, idUsuario);
            var erros = ValidaGasto(gasto);

            if (erros.Any()) return BadRequest(erros);

            _context.Gasto.Add(gasto);
            _context.SaveChanges();

            return Created($"api/gastos/{gasto.Id}", gasto);
        }

        [HttpDelete("id")]
        public IActionResult Deletar(int id)
        {
            var gasto = _context.Gasto.Find(id);

            if (gasto == null) return BadRequest("Gasto não encontrado!");

            _context.Gasto.Remove(gasto);
            _context.SaveChanges();

            return Ok("Gasto removido!");
        }

        [HttpPut("id")]
        public IActionResult Editar(int id, string descricao, decimal valor, int categoriaId, int usuarioId)
        {
            var gasto = _context.Gasto.Find(id);

            if (gasto == null) return BadRequest("Gasto não encontrado.");

            gasto.Descricao = descricao;
            gasto.Valor = valor;
            gasto.CategoriaId = categoriaId;
            gasto.UsuarioId = usuarioId;

            var erros = ValidaGasto(gasto);
            if (erros.Any()) return BadRequest(erros);

            _context.SaveChanges();
            return Ok("Gasto editado!");
        }

        private List<string> ValidaGasto(Gasto gasto)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(gasto.Descricao)) erros.Add("Descrição é obrigatória.");
            if (gasto.Valor <= 0) erros.Add("O valor inválido.");
            if (_context.Gasto.Any(g => g.CategoriaId != gasto.CategoriaId)) erros.Add("Categoria não encontrada.");
            if (_context.Gasto.Any(g => g.UsuarioId != gasto.UsuarioId)) erros.Add("Usuário não encontrado.");
            return erros;
        }
    }
}