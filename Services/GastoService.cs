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


        public IEnumerable<GastoResponseDto> Listar()
        {
            return _repository.Listar().Select(g => new GastoResponseDto
            {
                Id = g.Id,
                Descricao = g.Descricao,
                Valor = g.Valor,
                CategoriaId = g.CategoriaId
            });
        }

        public Resultado<GastoResponseDto> Criar(CriarGastoDto dto)
        {
            Gasto gasto = new Gasto(dto.Descricao, dto.Valor, dto.CategoriaId, dto.UsuarioId);
            var erros = ValidarGasto(gasto);

            if (erros.Any()) return Resultado<GastoResponseDto>.Erro(erros);

            _repository.Adicionar(gasto);

            GastoResponseDto resposta = new GastoResponseDto
            {
                Id = gasto.Id,
                Descricao = gasto.Descricao,
                Valor = gasto.Valor,
                CategoriaId = gasto.CategoriaId
            };

            return Resultado<GastoResponseDto>.Ok(resposta);
        }

        public Resultado<Gasto> Remover(int id)
        {
            var gasto = _repository.BuscarPorId(id);

            if (gasto == null) return Resultado<Gasto>.Erro("Gasto não encontrado!");

            _repository.Remover(gasto);

            return Resultado<Gasto>.Ok();
        }


        public Resultado<GastoResponseDto> Editar(int id, EditarGastoDto dto)
        {
            var gasto = _repository.BuscarPorId(id);

            if (gasto == null) return Resultado<GastoResponseDto>.Erro("Gasto não Encontrado!");

            gasto.Descricao = dto.Descricao;
            gasto.Valor = dto.Valor;
            gasto.CategoriaId = dto.CategoriaId;

            var erros = ValidarGasto(gasto);
            if (erros.Any()) return Resultado<GastoResponseDto>.Erro(erros);

            _repository.Salvar();

            GastoResponseDto resposta = new GastoResponseDto
            {
                Id = gasto.Id,
                Descricao = gasto.Descricao,
                Valor = gasto.Valor,
                CategoriaId = gasto.CategoriaId
            };

            return Resultado<GastoResponseDto>.Ok(resposta);
        }

        private List<string> ValidarGasto(Gasto gasto)
        {
            var erros = new List<string>();

            if (!_repository.ExisteCategoria(gasto.CategoriaId)) erros.Add("Categoria não encontrada.");
            if (!_repository.ExisteUsuario(gasto.UsuarioId)) erros.Add("Usuário não encontrado.");

            return erros;
        }
    }
}