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
    public class MaterialesDidacticosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialesDidacticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MaterialDidactico.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.MaterialDidactico
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null) return NotFound();

            return View(material);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialDidactico material)
        {
            bool existe = _context.MaterialDidactico
                .Any(m => m.Titulo == material.Titulo &&
                          m.Tipo == material.Tipo);

            if (existe)
                ModelState.AddModelError("", "Ya existe un material con ese título y tipo");

            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.MaterialDidactico.FindAsync(id);
            if (material == null) return NotFound();

            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaterialDidactico material)
        {
            if (id != material.Id) return NotFound();

            bool existe = _context.MaterialDidactico
                .Any(m => m.Titulo == material.Titulo &&
                          m.Tipo == material.Tipo &&
                          m.Id != material.Id);

            if (existe)
                ModelState.AddModelError("", "Ya existe otro material con ese título y tipo");

            if (ModelState.IsValid)
            {
                _context.Update(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.MaterialDidactico
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null) return NotFound();

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.MaterialDidactico.FindAsync(id);

            if (material != null)
                _context.MaterialDidactico.Remove(material);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}