using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<GastoResponseDto>> Listar()
        {
            var gastos = _service.Listar();

            return Ok(gastos);
        }

        [HttpPost]
        public ActionResult<GastoResponseDto> Criar(CriarGastoDto dto)
        {
            var resultado = _service.Criar(dto);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return Created($"api/gastos/{resultado.Dado!.Id}", resultado.Dado);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var resultado = _service.Remover(id);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<GastoResponseDto> Editar(int id, EditarGastoDto dto)
        {
            var resultado = _service.Editar(id, dto);

            if (!resultado.Sucesso) return NotFound(resultado.Erros);
           
            return Ok(resultado.Dado);
        }

        
    }
}