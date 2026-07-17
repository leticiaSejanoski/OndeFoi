using Microsoft.AspNetCore.Http.HttpResults;
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

        public Resultado Criar(string nome)
        {
            Categoria categoria = new Categoria(nome);

            var erros = ValidarCategoria(categoria);

            if (erros.Any()) return Resultado.Erro(erros);

            _repository.Adicionar(categoria);

            return Resultado.Ok(categoria);
        }

        public Resultado Remover(int id)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado.Erro("Categoria não encontrada.");

            _repository.Remover(categoria);

            return Resultado.Ok();

        }

        public Resultado Editar(int id, string nome)
        {
            var categoria = _repository.BuscarPorId(id);

            if (categoria == null) return Resultado.Erro("Categoria não encontrada.");

            categoria.Nome = nome;
            var erros = ValidarCategoria(categoria, id);
            if (erros.Any()) return Resultado.Erro(erros);

            _repository.Salvar();

            return Resultado.Ok(categoria);
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