using OndeFoi.DTOs;
using OndeFoi.Models;
using OndeFoi.Repositories;

namespace OndeFoi.Services
{
    public class GastoService
    {
        private readonly GastoRepository _repository;

        public GastoService(GastoRepository repository)
        {
            _repository = repository;
        }


        public List<Gasto> Listar()
        {
            return _repository.Listar();
        }

        public Resultado<Gasto> Criar(CriarGastoDto dto)
        {
            Gasto gasto = new Gasto(dto.Descricao, dto.Valor, dto.CategoriaId, dto.UsuarioId);
            var erros = ValidarGasto(gasto);

            if (erros.Any()) return Resultado<Gasto>.Erro(erros);

            _repository.Adicionar(gasto);

            return Resultado<Gasto>.Ok(gasto);
        }

        public Resultado<Gasto> Remover(int id)
        {
            var gasto = _repository.BuscarPorId(id);

            if (gasto == null) return Resultado<Gasto>.Erro("Gasto não encontrado!");

            _repository.Remover(gasto);

            return Resultado<Gasto>.Ok();
        }


         public Resultado<Gasto> Editar(int id, EditarGastoDto dto)
        {
            var gasto = _repository.BuscarPorId(id);

            if (gasto == null) return Resultado<Gasto>.Erro("Gasto não Encontrado!");

            gasto.Descricao = dto.Descricao;
            gasto.Valor = dto.Valor;
            gasto.CategoriaId = dto.CategoriaId;

            var erros = ValidarGasto(gasto);
            if (erros.Any()) return Resultado<Gasto>.Erro(erros);

            _repository.Salvar();

            return Resultado<Gasto>.Ok();
        }
        
        private List<string> ValidarGasto(Gasto gasto)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(gasto.Descricao)) erros.Add("Descrição é obrigatória.");
            if (gasto.Valor <= 0) erros.Add("O valor inválido.");
            if (_repository.ExisteCategoria(gasto.CategoriaId)) erros.Add("Categoria não encontrada.");
            if (_repository.ExisteUsuario(gasto.UsuarioId)) erros.Add("Usuário não encontrado.");

            return erros;
        }
    }
}