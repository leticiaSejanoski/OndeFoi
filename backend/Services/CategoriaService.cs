using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        public IEnumerable<CategoriaResponseDto> Listar(int usuarioId)
        {
            return _repository.Listar(usuarioId).Select(c => new CategoriaResponseDto
            {
                Id = c.Id,
                Nome = c.Nome
            });
        }

        public Resultado<CategoriaResponseDto> Criar(CriarCategoriaDto dto, int usuarioId)
        {
            Categoria categoria = new Categoria(dto.Nome, usuarioId);

            var erros = ValidarCategoria(categoria, usuarioId);

            if (erros.Any()) return Resultado<CategoriaResponseDto>.Erro(erros);

            _repository.Adicionar(categoria);

            CategoriaResponseDto resposta = new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Resultado<CategoriaResponseDto>.Ok(resposta);
        }

        public Resultado<Categoria> Remover(int id, int usuarioId)
        {
            var categoria = _repository.BuscarPorId(id, usuarioId);

            if (categoria == null) return Resultado<Categoria>.Erro("Categoria","Categoria não encontrada.");

            _repository.Remover(categoria);

            return Resultado<Categoria>.Ok();

        }

        public Resultado<CategoriaResponseDto> Editar(int id, EditarCategoriaDto dto, int usuarioId)
        {

            var categoria = _repository.BuscarPorId(id, usuarioId);

            if (categoria == null) return Resultado<CategoriaResponseDto>.Erro("Categoria","Categoria não encontrada.");

            categoria.Nome = dto.Nome;
            var erros = ValidarCategoria(categoria, usuarioId, id);
            if (erros.Any()) return Resultado<CategoriaResponseDto>.Erro(erros);

            _repository.Salvar();

            CategoriaResponseDto resposta = new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Resultado<CategoriaResponseDto>.Ok(resposta);
        }

        private Dictionary<string, string> ValidarCategoria(Categoria categoria, int usuarioId, int? id = null)
        {
            var erros = new Dictionary<string, string>();

            if (_repository.ExisteCategoriaComNome(categoria.Nome, usuarioId, id))
            {
                erros.Add("Categoria", "Categoria já existe.");
            }
            return erros;
        }

      
    }
}