
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Persistencia;

namespace GrowerTech_MVC.Repository
{
    public class AgricultorRepository
    {
        private readonly GrowerTechDbContext _context;

        public AgricultorRepository(GrowerTechDbContext context)
        {
            _context = context;
        }

        public List<Agricultor> GetAgricultores()
        {
            return _context.Agricultores.ToList();
        }

        public void AddAgricultor(Agricultor agricultor)
        {
            _context.Agricultores.Add(agricultor);
            _context.SaveChanges();
        }
    }
}
