using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.Models;
using OndeFoi.Data;
using OndeFoi.Repositories;
using OndeFoi.Services;

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

        [HttpGet]
        public IActionResult Listar()
        {
            var categorias = _service.Listar();

            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult Criar(string nome)
        {
            var resultado = _service.Criar(nome);

            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Created($"api/categorias/{resultado.Categoria!.Id}", resultado.Categoria);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var resultado = _service.Remover(id);
            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok("Categoria removida!");
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, string nome)
        {
            var resultado = _service.Editar(id, nome);
            if (!resultado.Sucesso) return BadRequest(resultado.Erros);

            return Ok("Categoria editada com sucesso!");
        }

    }
}