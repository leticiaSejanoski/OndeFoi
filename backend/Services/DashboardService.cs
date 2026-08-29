using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.DTOs;
using OndeFoi.Models;
using OndeFoi.Repositories;

namespace OndeFoi.Services
{
    public class DashboardService
    {
        private readonly UsuarioRepository _repositoryUsuario;
        private readonly GastoRepository _repositoryGasto;

        public DashboardService(UsuarioRepository repositoryUsuario, GastoRepository repositoryGasto)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryGasto = repositoryGasto;
        }

        public async Task<Resultado<DashboardResponseDto>> Resumo(int usuarioId)
        {
            var usuario = _repositoryUsuario.BuscarPorId(usuarioId);

            if (usuario == null) return Resultado<DashboardResponseDto>.Erro("Usuario", "Usuário não encontrado!");

            var renda = usuario.Renda;
            var totalGastos = await CalcularTotalGrupo(usuarioId);
            var totalMesAtual = totalGastos;
            var saldo = renda - totalMesAtual;


            var totalCategoriaAgrupado = await CalcularTotalCategoriaGrupo(usuarioId);

            DashboardResponseDto resposta = new DashboardResponseDto
            {
                Renda = renda,
                Saldo = saldo,
                TotalGastosMesAtual = totalMesAtual,
                TotalPorCategoria = totalCategoriaAgrupado
            };

            return Resultado<DashboardResponseDto>.Ok(resposta);
        }

        public async Task<decimal> CalcularTotalGrupo(int idUsuario)
        {
            var gastos = await _repositoryGasto.BuscarGastosUltimoMes(idUsuario);

            return gastos.Sum(g => g.Valor); ;
        }

        public async Task<IEnumerable<GastoPorCategoria>> CalcularTotalCategoriaGrupo(int idUsuario)
        {
            var gastos = await _repositoryGasto.BuscarGastosUltimoMes(idUsuario);

            var resposta = gastos
            .GroupBy(g => new
            {
                g.Categoria.Nome
            })
            .Select(grupo => new GastoPorCategoria
            {
                CategoriaNome = grupo.Key.Nome,
                Total = grupo.Sum(g => g.Valor)
            });

            return resposta;

        }
    }
}