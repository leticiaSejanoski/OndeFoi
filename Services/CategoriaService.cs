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


        public List<Categoria> Listar()
        {
            return _repository.Listar();
        }

        public Resultado<Categoria> Criar(CriarCategoriaDto dto)
        {
            Categoria categoria = new Categoria(dto.Nome);

            var erros = ValidarCategoria(categoria);

            if (erros.Any()) return Resultado<Categoria>.Erro(erros);

            _repository.Adicionar(categoria);

            return Resultado<Categoria>.Ok(categoria);
        }

        public Resultado<Categoria> Remover(int id)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado<Categoria>.Erro("Categoria não encontrada.");

            _repository.Remover(categoria);

            return Resultado<Categoria>.Ok();

        }

        public Resultado<Categoria> Editar(int id, CriarCategoriaDto dto)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado<Categoria>.Erro("Categoria não encontrada.");

            categoria.Nome = dto.Nome;
            var erros = ValidarCategoria(categoria, id);
            if (erros.Any()) return Resultado<Categoria>.Erro(erros);

            _repository.Salvar();

            return Resultado<Categoria>.Ok(categoria);
        }

        private List<string> ValidarCategoria(Categoria categoria, int? id = null)
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(categoria.Nome)) erros.Add("Nome é obrigatório.");
            if (_repository.ExisteCategoriaComNome(categoria.Nome, id))
            {
                erros.Add("Categoria já existe.");
            }
            return erros;
        }
    }
}