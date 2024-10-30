using GrowerTech_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowerTech_MVC.Persistencia
{
    public class GrowerTechDbContext: DbContext
    {
        public DbSet<Agricultor> Agricultores { get; set; }
        public DbSet<DadoClimatico> DadoClimaticos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }

        public GrowerTechDbContext(DbContextOptions<GrowerTechDbContext> options) : base(options)
        {

        }
    }
}
