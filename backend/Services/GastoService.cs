using Microsoft.AspNetCore.Http.HttpResults;
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

        public IEnumerable<GastoResponseDto> Listar(int usuarioId)
        {
            return _repository.Listar(usuarioId).Select(g => new GastoResponseDto
            {
                Id = g.Id,
                Descricao = g.Descricao,
                Valor = g.Valor,
                Data = g.Data,
                CategoriaId = g.CategoriaId,
                CategoriaNome = g.Categoria.Nome
            });
        }

        public Resultado<GastoResponseDto> Criar(CriarGastoDto dto, int usuarioId)
        {
            Gasto gasto = new Gasto(dto.Descricao, dto.Valor, dto.Data ?? DateTime.Now, dto.CategoriaId, usuarioId); 
            var erros = ValidarGasto(gasto, usuarioId);

            if (erros.Any()) return Resultado<GastoResponseDto>.Erro(erros);

            _repository.Adicionar(gasto);

            gasto = _repository.BuscarPorId(gasto.Id, usuarioId);
            Console.WriteLine(gasto.Categoria == null);


            GastoResponseDto resposta = new GastoResponseDto
            {
                Id = gasto.Id,
                Descricao = gasto.Descricao,
                Valor = gasto.Valor,
                Data = gasto.Data,
                CategoriaId = gasto.CategoriaId,
                CategoriaNome = gasto.Categoria.Nome
            };


            return Resultado<GastoResponseDto>.Ok(resposta);
        }

        public Resultado<Gasto> Remover(int id, int usuarioId)
        {
            var gasto = _repository.BuscarPorId(id, usuarioId);

            if (gasto == null) return Resultado<Gasto>.Erro("Gasto", "Gasto não encontrado!");

            _repository.Remover(gasto);

            return Resultado<Gasto>.Ok();
        }


        public Resultado<GastoResponseDto> Editar(int id, int usuarioId, EditarGastoDto dto)
        {
            var gasto = _repository.BuscarPorId(id, usuarioId);

            if (gasto == null) return Resultado<GastoResponseDto>.Erro("Gasto", "Gasto não Encontrado!");

            gasto.Descricao = dto.Descricao;
            gasto.Valor = dto.Valor;
            gasto.Data = dto.Data ?? DateTime.Now;
            gasto.CategoriaId = dto.CategoriaId;

            var erros = ValidarGasto(gasto, usuarioId);
            if (erros.Any()) return Resultado<GastoResponseDto>.Erro(erros);

            _repository.Salvar();

            GastoResponseDto resposta = new GastoResponseDto
            {
                Id = gasto.Id,
                Descricao = gasto.Descricao,
                Valor = gasto.Valor,
                Data = gasto.Data,
                CategoriaId = gasto.CategoriaId,
                CategoriaNome = gasto.Categoria.Nome

            };

            return Resultado<GastoResponseDto>.Ok(resposta);
        }

        public async Task<IEnumerable<GastosPorMesResponseDto>> GastosAgrupados(int idUsuario)
        {
            var gastos = await _repository.BuscarGastosTodosOsMeses(idUsuario);

            var resposta = gastos.GroupBy(g => new
            {
                g.Data.Year,
                g.Data.Month
            })
            .OrderByDescending(grupo => grupo.Key.Year)
            .ThenByDescending(grupo => grupo.Key.Month)
            .Select(gp => new GastosPorMesResponseDto
            {
                Ano = gp.Key.Year,
                Mes = gp.Key.Month,

                Gastos = gp.Select(g => new GastoResponseDto
                {
                    Id = g.Id,
                    Descricao = g.Descricao,
                    Valor = g.Valor,
                    Data = g.Data,
                    CategoriaId = g.CategoriaId,
                    CategoriaNome = g.Categoria.Nome

                }).ToList(),

                Total = gp.Sum(g => g.Valor)
            });

            return resposta;
        }

        public async Task<Resultado<Gasto>> ExcluirGastosAgrupados(int idUsuario, int mesGrupo, int anoGrupo)
        {
            var gastos = await _repository.BuscarGastosMes(idUsuario, mesGrupo, anoGrupo);

            foreach (var gasto in gastos)
            {
                _repository.Remover(gasto);
            }

            return Resultado<Gasto>.Ok();
        }


        private Dictionary<string, string> ValidarGasto(Gasto gasto, int usuarioId)
        {
            var erros = new Dictionary<string, string>();

            if (!_repository.ExisteCategoria(gasto.CategoriaId, usuarioId)) erros.Add("Categoria", "Categoria não encontrada.");

            if (gasto.Data.Date > DateTime.Today) erros.Add("Data", "A data não pode ser futura.");
            return erros;
        }
    }
}