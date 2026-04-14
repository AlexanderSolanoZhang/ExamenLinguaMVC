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
    public class InstructoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructor.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructor.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            bool existe = _context.Instructor
                .Any(i => i.Correo == instructor.Correo);

            if (existe)
                ModelState.AddModelError("Correo", "Correo duplicado");

            if (instructor.AniosExperiencia < 0)
                ModelState.AddModelError("AniosExperiencia", "No puede ser negativo");

            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(instructor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id) return NotFound();

            bool existe = _context.Instructor
                .Any(i => i.Correo == instructor.Correo && i.Id != instructor.Id);

            if (existe)
                ModelState.AddModelError("Correo", "Correo duplicado");

            if (instructor.AniosExperiencia < 0)
                ModelState.AddModelError("AniosExperiencia", "No puede ser negativo");

            if (ModelState.IsValid)
            {
                _context.Update(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(instructor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructor.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor != null)
                _context.Instructor.Remove(instructor);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}