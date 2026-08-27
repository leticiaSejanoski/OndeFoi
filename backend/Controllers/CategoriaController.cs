using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Models;
using OndeFoi.Data;
using OndeFoi.Repositories;
using OndeFoi.Services;
using OndeFoi.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaResponseDto>> Listar()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var categorias = _service.Listar(usuarioId);

            return Ok(categorias);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<CategoriaResponseDto> Criar(CriarCategoriaDto dto)
        {

            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Criar(dto, usuarioId);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Created($"api/categorias/{resultado.Dado!.Id}", resultado.Dado);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Remover(id, usuarioId);
            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<CategoriaResponseDto> Editar(int id, EditarCategoriaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Editar(id, dto, usuarioId);
            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok(resultado.Dado);
        }


        private Dictionary<string, string> ObterErrosModelState()
        {
            return ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value.Errors.First().ErrorMessage);
        }
    }
}