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
            .OrderBy(g => g.Descricao)
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
        
         public bool ExisteUsuario(int usuarioId)
        {
            return _context.Usuario.Any(u => u.Id == usuarioId);
        }
       
    }
}