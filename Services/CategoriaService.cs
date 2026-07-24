using Microsoft.AspNetCore.Http.HttpResults;
using OndeFoi.DTOs;
using OndeFoi.Models;
using OndeFoi.Repositories;

namespace OndeFoi.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repository;

        public CategoriaService(CategoriaRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<CategoriaResponseDto> Listar()
        {
            return _repository.Listar().Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nome = c.Nome
            });
        }

        public Resultado<CategoriaResponseDto> Criar(CriarCategoriaDto dto)
        {
            Categoria categoria = new Categoria(dto.Nome);

            var erros = ValidarCategoria(categoria);

            if (erros.Any()) return Resultado<CategoriaResponseDto>.Erro(erros);

            _repository.Adicionar(categoria);

            CategoriaResponseDto resposta = new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Resultado<CategoriaResponseDto>.Ok(resposta);
        }

        public Resultado<Categoria> Remover(int id)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado<Categoria>.Erro("Categoria não encontrada.");

            _repository.Remover(categoria);

            return Resultado<Categoria>.Ok();

        }

        public Resultado<CategoriaResponseDto> Editar(int id, EditarCategoriaDto dto)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado<CategoriaResponseDto>.Erro("Categoria não encontrada.");

            categoria.Nome = dto.Nome;
            var erros = ValidarCategoria(categoria, id);
            if (erros.Any()) return Resultado<CategoriaResponseDto>.Erro(erros);

            _repository.Salvar();

            CategoriaResponseDto resposta = new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Resultado<CategoriaResponseDto>.Ok(resposta);
        }

        private List<string> ValidarCategoria(Categoria categoria, int? id = null)
        {
            var erros = new List<string>();

            if (_repository.ExisteCategoriaComNome(categoria.Nome, id))
            {
                erros.Add("Categoria já existe.");
            }
            return erros;
        }
    }
}