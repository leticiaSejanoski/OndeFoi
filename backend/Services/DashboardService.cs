using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.DTOs;
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

        public Resultado<DashboardResponseDto> Resumo(int usuarioId)
        {
            var usuario = _repositoryUsuario.BuscarPorId(usuarioId);

            if (usuario == null) return Resultado<DashboardResponseDto>.Erro("usuario", "Usuário não encontrado!");

            var renda = usuario.Renda;
            var totalGastos = _repositoryGasto.CalcularTotalGasto(usuarioId);
            var saldo = renda - totalGastos;

            DashboardResponseDto resposta = new DashboardResponseDto
            {
                Renda = renda,
                TotalGastos = totalGastos,
                Saldo = saldo
            };

            return Resultado<DashboardResponseDto>.Ok(resposta);

        }

    }
}