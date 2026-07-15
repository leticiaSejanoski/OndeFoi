using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Models;
using OndeFoi.Data;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var categorias = _context.Categoria.Select(c => new
            {
                c.Nome
            })
            .OrderBy(c => c.Nome)
            .ToList();

            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult Criar(string nome)
        {
            Categoria categoria = new Categoria(nome);

            var erros = validaCategoria(categoria);

            if (erros.Any()) return BadRequest(erros);

            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return Created($"api/categorias/{categoria.Id}", categoria);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var categoria = _context.Categoria.Find(id);
            if (categoria == null) return BadRequest("Categoria não encontrada!");

            _context.Categoria.Remove(categoria);
            _context.SaveChanges();

            return Ok("Categoria removida!");
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, string nome)
        {
            var categoria = _context.Categoria.Find(id);
            if (categoria == null) return BadRequest("Categoria não encontrada!");

            categoria.Nome = nome;
            var erros = validaCategoria(categoria, id);
            if (erros.Any()) return BadRequest(erros);

            _context.SaveChanges();
            return Ok("Categoria editada com sucesso!");
        }

        private List<string> validaCategoria(Categoria categoria, int? id = null)
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(categoria.Nome)) erros.Add("Nome é obrigatório.");
            if (_context.Categoria.Any(c => c.Nome.ToLower() == categoria.Nome.ToLower() && (id == null || c.Id != id)))
            {
                erros.Add("Categoria já existe.");
            }
            return erros;
        }
    }
}