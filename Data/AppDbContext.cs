
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OndeFoi.Models;

namespace OndeFoi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        
    }

}