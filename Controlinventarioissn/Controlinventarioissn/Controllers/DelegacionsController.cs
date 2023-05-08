using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;

namespace Controlinventarioissn.Controllers
{
    public class DelegacionsController : Controller
    {
        private readonly DataContext _context;

        public DelegacionsController(DataContext context)
        {
            _context = context;
        }

        // GET: Delegacions
        public async Task<IActionResult> Index()
        {
              return _context.Delegaciones != null ? 
                          View(await _context.Delegaciones.ToListAsync()) :
                          Problem("Entity set 'DataContext.Delegaciones'  is null.");
        }

        // GET: Delegacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            var delegacion = await _context.Delegaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegacion == null)
            {
                return NotFound();
            }

            return View(delegacion);
        }

        // GET: Delegacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delegacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Delegacion delegacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delegacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delegacion);
        }

        // GET: Delegacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            var delegacion = await _context.Delegaciones.FindAsync(id);
            if (delegacion == null)
            {
                return NotFound();
            }
            return View(delegacion);
        }

        // POST: Delegacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Delegacion delegacion)
        {
            if (id != delegacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delegacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelegacionExists(delegacion.Id))
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
            return View(delegacion);
        }

        // GET: Delegacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            var delegacion = await _context.Delegaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegacion == null)
            {
                return NotFound();
            }

            return View(delegacion);
        }

        // POST: Delegacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Delegaciones == null)
            {
                return Problem("Entity set 'DataContext.Delegaciones'  is null.");
            }
            var delegacion = await _context.Delegaciones.FindAsync(id);
            if (delegacion != null)
            {
                _context.Delegaciones.Remove(delegacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelegacionExists(int id)
        {
          return (_context.Delegaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
