
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Data;
using OndeFoi.Models;
using OndeFoi.Services;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _service.Listar();

            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string senha)
        {
            var resultado = _service.Cadastrar(nome, email, senha);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);
            return Created($"api/usuarios/{resultado.Dado!.Id}", resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var resultado = _service.Deletar(id);
            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok("Usuário deletado!");
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, string nome, string email, string senha)
        {
            var resultado = _service.Editar(id, nome, email, senha);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok("Informações alteradas com sucesso.");
        }
    }
}