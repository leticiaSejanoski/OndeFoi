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

        public List<Usuario> Listar()
        {
            return _context.Usuario
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

        public bool ExisteEmail(string email, int? id = null)
        {
            return _context.Usuario.Any(e => e.Email.ToLower() == email.ToLower() && (id == null || e.Id != id));
        }
    }
}