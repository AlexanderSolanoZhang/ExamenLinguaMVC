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
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Curso.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Curso.FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (curso.Costo <= 0)
                ModelState.AddModelError("Costo", "El costo debe ser mayor a 0");

            bool existe = _context.Curso
                .Any(c => c.Nombre == curso.Nombre && c.Idioma == curso.Idioma);

            if (existe)
                ModelState.AddModelError("", "Ya existe un curso con ese nombre e idioma");

            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(curso);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id) return NotFound();

            if (curso.Costo <= 0)
                ModelState.AddModelError("Costo", "El costo debe ser mayor a 0");

            bool existe = _context.Curso
                .Any(c => c.Nombre == curso.Nombre &&
                          c.Idioma == curso.Idioma &&
                          c.Id != curso.Id);

            if (existe)
                ModelState.AddModelError("", "Ya existe un curso con ese nombre e idioma");

            if (ModelState.IsValid)
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(curso);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Curso.FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            if (curso != null)
                _context.Curso.Remove(curso);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}