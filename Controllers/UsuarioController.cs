
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Data;
using OndeFoi.DTOs;
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

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioResponseDto>> Listar()
        {
            var usuarios = _service.Listar();

            return Ok(usuarios);
        }

        [AllowAnonymous]
        [HttpPost("cadastro")]
        public ActionResult<UsuarioResponseDto> Cadastrar(CadastrarUsuarioDto dto)
        {
            var resultado = _service.Cadastrar(dto);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);
            return Created($"api/usuarios/{resultado.Dado!.Id}", resultado.Dado);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var resultado = _service.Deletar(id);
            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<UsuarioResponseDto> Editar(int id, EditarUsuarioDto dto)
        {
            var resultado = _service.Editar(id, dto);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok(resultado.Dado);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login(LoginDto dto)
        {
            var resultado = _service.Login(dto);

            if (!resultado.Sucesso) return Unauthorized(resultado.Erros);

            return Ok(resultado.Dado);
        }
    }
}