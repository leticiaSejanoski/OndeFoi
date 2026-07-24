using OndeFoi.Data;
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

        public List<Gasto> Listar()
        {
            return _context.Gasto
            .OrderBy(g => g.Descricao)
            .ToList();
        }

        public void Adicionar(Gasto gasto)
        {
            _context.Gasto.Add(gasto);
            _context.SaveChanges();
        }

        public Gasto? BuscarPorId(int id)
        {
            return _context.Gasto.Find(id);
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

        public bool ExisteCategoria(int categoriaId)
        {
            return _context.Categoria.Any(c => c.Id == categoriaId);
        }
        
         public bool ExisteUsuario(int usuarioId)
        {
            return _context.Usuario.Any(u => u.Id == usuarioId);
        }
       
    }
}