using Microsoft.EntityFrameworkCore;
using OndeFoi.Data;
using OndeFoi.Models;

namespace OndeFoi.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar(int usuarioId)
        {
            return _context.Usuario
            .Where(u => u.Id == usuarioId)
           .OrderBy(u => u.Nome)
           .ToList();
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Remover(Usuario usuario)
        {
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public Usuario? BuscarPorId(int id)
        {
            return _context.Usuario.Find(id);
        }
        
         public Usuario? BuscarPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(u => u.Email == email);
        }

        public bool ExisteEmail(string email, int? id = null)
        {
            return _context.Usuario.Any(e => e.Email.ToLower() == email.ToLower() && (id == null || e.Id != id));
        }

        public RefreshToken? BuscarRefreshToken(string refreshToken)
        {
            return _context.RefreshToken
            .Include(r => r.Usuario)
            .FirstOrDefault(r => r.Token == refreshToken);   
        }

        public void AdicionarRefreshToken(RefreshToken refreshToken)
        {
            _context.Add(refreshToken);
            _context.SaveChanges();
        }
    }
}