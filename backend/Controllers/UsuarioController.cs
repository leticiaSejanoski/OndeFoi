
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.DTOs;
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
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var usuarios = _service.Listar(usuarioId);

            return Ok(usuarios);
        }

        [AllowAnonymous]
        [HttpPost("cadastro")]
        public ActionResult<UsuarioResponseDto> Cadastrar(CadastrarUsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var resultado = _service.Cadastrar(dto);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Created($"api/usuarios/{resultado.Dado!.Id}", resultado.Dado);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Deletar()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Deletar(usuarioId);
            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [Authorize]
        [HttpPut]
        public ActionResult<UsuarioResponseDto> Editar(EditarUsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Editar(usuarioId, dto);

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


        [Authorize]
        [HttpPut("renda")]
        public ActionResult Renda(EditarRendaDto dto)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.EditarRenda(usuarioId, dto);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok(resultado.Dado);
        }


        [HttpPost("refresh")]
        public IActionResult Refresh(string refreshToken)
        {
            var resultado = _service.RenovarToken(refreshToken);

            if (!resultado.Sucesso) return Unauthorized(resultado.Erros);

            return Ok(resultado.Dado);
        }

          private Dictionary<string, string> ObterErrosModelState()
        {
            return ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value.Errors.First().ErrorMessage
            );
        }
    }
}