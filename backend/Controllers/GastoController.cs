using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OndeFoi.Data;
using OndeFoi.DTOs;
using OndeFoi.Models;
using OndeFoi.Services;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {

        private readonly GastoService _service;

        public GastosController(GastoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<GastoResponseDto>> Listar()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var gastos = _service.Listar(usuarioId);

            return Ok(gastos);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<GastoResponseDto> Criar(CriarGastoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Criar(dto, usuarioId);


            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return Created($"api/gastos/{resultado.Dado!.Id}", resultado.Dado);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Remover(id, usuarioId);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("historico/mes")]
        public async Task<IActionResult>  Deletar(int mes, int ano)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = await _service.ExcluirGastosAgrupados(usuarioId, mes, ano);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<GastoResponseDto> Editar(int id, EditarGastoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ObterErrosModelState());

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Editar(id, usuarioId, dto);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return Ok(resultado.Dado);
        }

        [Authorize]
        [HttpGet("historico")]
        public async Task<ActionResult<IEnumerable<GastoResponseDto>>> Historico()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var resultado = await _service.GastosAgrupados(usuarioId);

            return Ok(resultado);
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