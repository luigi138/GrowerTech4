using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Persistencia;

namespace _2TDSPM.Controllers
{
    public class AgricultoresController : Controller
    {
        private readonly GrowerTechDbContext _context;

        public AgricultoresController(GrowerTechDbContext context)
        {
            _context = context;
        }

        // GET: Agricultores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agricultores.ToListAsync());
        }

        // GET: Agricultores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agricultor = await _context.Agricultores
                .FirstOrDefaultAsync(m => m.AgricultorId == id);
            if (agricultor == null)
            {
                return NotFound();
            }

            return View(agricultor);
        }

        // GET: Agricultores/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgricultorId,Nome,Escala, Endereço")] Agricultor agricultor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agricultor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agricultor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agricultor = await _context.Agricultores.FindAsync(id);
            if (agricultor == null)
            {
                return NotFound();
            }
            return View(agricultor);
        }

        // POST: Agricultores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgricultorId,Nome,Escala, Endereço")] Agricultor agricultor)
        {
            if (id != agricultor.AgricultorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agricultor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgricultorExists(agricultor.AgricultorId))
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
            return View(agricultor);
        }

        // GET: Agricultores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agricultor = await _context.Agricultores
                .FirstOrDefaultAsync(m => m.AgricultorId == id);
            if (agricultor == null)
            {
                return NotFound();
            }

            return View(agricultor);
        }

        // POST: Agricultores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agricultor = await _context.Agricultores.FindAsync(id);
            if (agricultor != null)
            {
                _context.Agricultores.Remove(agricultor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgricultorExists(int id)
        {
            return _context.Agricultores.Any(e => e.AgricultorId == id);
        }
    }
}