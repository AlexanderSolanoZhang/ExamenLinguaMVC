using ExamenLinguaMVC.Data;
using ExamenLinguaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenLinguaMVC.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class NivelesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NivelesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Nivel.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var nivel = await _context.Nivel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nivel == null) return NotFound();

            return View(nivel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nivel nivel)
        {
            bool existe = _context.Nivel
                .Any(n => n.Nombre.ToLower() == nivel.Nombre.ToLower());

            if (existe)
                ModelState.AddModelError("", "Ya existe un nivel con ese nombre");

            if (ModelState.IsValid)
            {
                _context.Add(nivel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(nivel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var nivel = await _context.Nivel.FindAsync(id);
            if (nivel == null) return NotFound();

            return View(nivel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nivel nivel)
        {
            if (id != nivel.Id) return NotFound();

            bool existe = _context.Nivel
                .Any(n => n.Nombre.ToLower() == nivel.Nombre.ToLower()
                       && n.Id != nivel.Id);

            if (existe)
                ModelState.AddModelError("", "Nombre de nivel duplicado");

            if (ModelState.IsValid)
            {
                _context.Update(nivel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(nivel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var nivel = await _context.Nivel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nivel == null) return NotFound();

            return View(nivel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nivel = await _context.Nivel.FindAsync(id);

            if (nivel != null)
                _context.Nivel.Remove(nivel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}