using OndeFoi.Data;
using OndeFoi.DTOs;
using OndeFoi.Models;

namespace OndeFoi.Repositories
{
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Listar(int usuarioId)
        {
            return _context.Categoria
            .Where(c => c.UsuarioId == usuarioId)
             .OrderBy(c => c.Nome)
             .ToList();
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
        }

        public Categoria? BuscarPorId(int id, int usuarioId)
        {
            return _context.Categoria
            .FirstOrDefault(c => c.Id == id && c.UsuarioId == usuarioId);
        }

        public void Remover(Categoria categoria)
        {
            _context.Remove(categoria);
            _context.SaveChanges();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public bool ExisteCategoriaComNome(string nome, int usuarioId, int? id = null)
        {
            return _context.Categoria
            .Where(c => c.UsuarioId == usuarioId)
            .Any(c => c.Nome.ToLower() == nome.ToLower() && (id == null || c.Id != id));

        }
    }


}