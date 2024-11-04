using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrowerTech_MVC.Data;
using GrowerTech_MVC.Models;

namespace GrowerTech_MVC.Controllers
{
    public class SensoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SensoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sensores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sensores.ToListAsync());
        }

        // GET: Sensores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensores
                .Include(s => s.DadosClimaticos)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Sensores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Localizacao")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        // GET: Sensores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return View(sensor);
        }

        // POST: Sensores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Localizacao")] Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        private bool SensorExists(int id)
        {
            return _context.Sensores.Any(e => e.Id == id);
        }
    }
}