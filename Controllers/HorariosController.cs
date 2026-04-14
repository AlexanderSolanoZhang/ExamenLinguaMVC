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
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Horario.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Horario horario)
        {
            bool existe = _context.Horario
                .Any(h => h.Dia == horario.Dia && h.HoraInicio == horario.HoraInicio);

            if (existe)
                ModelState.AddModelError("", "Ya existe un horario con ese día y hora");

            if (ModelState.IsValid)
            {
                _context.Add(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(horario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FindAsync(id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Horario horario)
        {
            if (id != horario.Id) return NotFound();

            bool existe = _context.Horario
                .Any(h => h.Dia == horario.Dia &&
                          h.HoraInicio == horario.HoraInicio &&
                          h.Id != horario.Id);

            if (existe)
                ModelState.AddModelError("", "Conflicto de horario");

            if (ModelState.IsValid)
            {
                _context.Update(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(horario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.Horario.FindAsync(id);
            if (horario != null)
                _context.Horario.Remove(horario);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}