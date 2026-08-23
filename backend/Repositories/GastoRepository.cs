using Microsoft.EntityFrameworkCore;
using OndeFoi.Data;
using OndeFoi.DTOs;
using OndeFoi.Models;
using OndeFoi.Services;

namespace OndeFoi.Repositories
{
    public class GastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gasto> Listar(int usuarioId)
        {
            return _context.Gasto
            .Where(g => g.UsuarioId == usuarioId)
            .Include(g => g.Categoria)
            .OrderBy(g => g.Data)
            .ToList();
        }

        public void Adicionar(Gasto gasto)
        {
            _context.Gasto.Add(gasto);
            _context.SaveChanges();
        }

        public Gasto? BuscarPorId(int id, int usuarioId)
        {
            return _context.Gasto
            .Include(g => g.Categoria)
            .FirstOrDefault(g => g.UsuarioId == usuarioId && g.Id == id);
        }

        public void Remover(Gasto gasto)
        {
            _context.Gasto.Remove(gasto);
            _context.SaveChanges();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public bool ExisteCategoria(int categoriaId, int usuarioId)
        {
            return _context.Categoria
            .Where(c => c.UsuarioId == usuarioId)
            .Any(c => c.Id == categoriaId);
        }

        public decimal CalcularTotalGasto(int idUsuario)
        {
            return _context.Gasto
            .Where(g => g.UsuarioId == idUsuario)
            .Sum(g => g.Valor);
        }

        public async Task<IEnumerable<Gasto>> BuscarGastosMesAtual(int idUsuario)
        {
            var dataAtual = DateTime.Now;

            return await _context.Gasto
            .Where(g => g.UsuarioId == idUsuario &&
            g.Data.Year == dataAtual.Year &&
            g.Data.Month == dataAtual.Month)
            .Include(g => g.Categoria)
            .ToListAsync();
        }

        public async Task<IEnumerable<Gasto>> BuscarGastosTodosOsMeses(int idUsuario)
        {
            return await _context.Gasto
            .Include(g => g.Categoria)
            .Where(g => g.UsuarioId == idUsuario)
            .ToListAsync();
        }

        public async Task<IEnumerable<Gasto>> BuscarGastosMes(int idUsuario, int mes, int ano)
        {
            return await _context.Gasto
            .Include(g => g.Categoria)
            .Where(g =>
            g.UsuarioId == idUsuario
            && g.Data.Month == mes
            && g.Data.Year == ano)
            .ToListAsync();
        }

        public IEnumerable<GastoPorCategoria> CalcularTotalCategoria(int usuarioId)
        {
            return _context.Gasto
            .Where(g => g.UsuarioId == usuarioId)
            .GroupBy(g => g.Categoria.Nome)
            .Select(grupo => new GastoPorCategoria
            {
                CategoriaNome = grupo.Key,
                Total = grupo.Sum(g => g.Valor)

            }).ToList();
        }

        // public void ExcluirGastosAgrupados(int usuarioId)
        // {
        //     return _context.Gasto.Remove()
        // }
    }
}