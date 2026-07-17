
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Data;
using OndeFoi.Models;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
  public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _context.Usuario.Select(u => new
            {
                u.Nome,
                u.Email
            })
            .OrderBy(u => u.Nome)
            .ToList();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string senha)
        {
            Usuario usuario = new Usuario(nome, email, senha);
            var erros = validaUsuario(usuario);
            if (erros.Any()) return BadRequest(erros);

            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return Created($"api/usuarios/{usuario.Id}", usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null) return BadRequest("Usuário não encontrado.");

            _context.Remove(usuario);
            _context.SaveChanges();
            return Ok("Usuário deletado!");
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, string nome, string email, string senha)
        {
            var usuario = _context.Usuario.Find(id);

            if (usuario == null) return BadRequest("Usuário não encontrado.");

            usuario.Nome = nome;
            usuario.Email = email;
            usuario.SenhaHash = senha;

            var erros = validaUsuario(usuario);
            if (erros.Any()) return BadRequest(erros);

            _context.SaveChanges();
            return Ok("Informações alteradas com sucesso.");
        }
        private List<string> validaUsuario(Usuario usuario)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Nome)) erros.Add("Nome é obrigatório!");
            if (string.IsNullOrWhiteSpace(usuario.Email)) erros.Add("Email é obrigatório!");
            if (!usuario.Email.Contains('@')) erros.Add("O e-mail deve conter '@'.");
            if (_context.Usuario.Any(u => u.Email == usuario.Email)) erros.Add("Esse email já está sendo usado.");
            if (string.IsNullOrWhiteSpace(usuario.SenhaHash)) erros.Add("Senha é obrigatória!");

            return erros;
        }
    }
}