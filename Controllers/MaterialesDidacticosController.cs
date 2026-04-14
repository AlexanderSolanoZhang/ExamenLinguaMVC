using ExamenLinguaMVC.Data;
using ExamenLinguaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenLinguaMVC.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MaterialesDidacticosController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public MaterialesDidacticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaterialesDidacticos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaterialDidactico.ToListAsync());
        }

        // GET: MaterialesDidacticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDidactico = await _context.MaterialDidactico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialDidactico == null)
            {
                return NotFound();
            }

            return View(materialDidactico);
        }

        // GET: MaterialesDidacticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialesDidacticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Tipo")] MaterialDidactico materialDidactico)
        {
            
                _context.Add(materialDidactico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(materialDidactico);
        }

        // GET: MaterialesDidacticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDidactico = await _context.MaterialDidactico.FindAsync(id);
            if (materialDidactico == null)
            {
                return NotFound();
            }
            return View(materialDidactico);
        }

        // POST: MaterialesDidacticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Tipo")] MaterialDidactico materialDidactico)
        {
            if (id != materialDidactico.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(materialDidactico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialDidacticoExists(materialDidactico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(materialDidactico);
        }

        // GET: MaterialesDidacticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialDidactico = await _context.MaterialDidactico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialDidactico == null)
            {
                return NotFound();
            }

            return View(materialDidactico);
        }

        // POST: MaterialesDidacticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialDidactico = await _context.MaterialDidactico.FindAsync(id);
            if (materialDidactico != null)
            {
                _context.MaterialDidactico.Remove(materialDidactico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialDidacticoExists(int id)
        {
            return _context.MaterialDidactico.Any(e => e.Id == id);
        }
    }
}
