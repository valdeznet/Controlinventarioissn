using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using System.Diagnostics.Metrics;

namespace Controlinventarioissn.Controllers
{
    public class DelegacionsController : Controller
    {
        private readonly DataContext _context;

        public DelegacionsController(DataContext context)
        {
            _context = context;
        }

        /******************************************************INDEX*****************************************************/
        public async Task<IActionResult> Index()
        {
              return _context.Delegaciones != null ? 
                          View(await _context.Delegaciones.ToListAsync()) :
                          Problem("Entity set 'DataContext.Delegaciones'  is null.");
        }

        /******************************************************DETALLES DELEGACIONES*****************************************************/
        public async Task<IActionResult> Details(int? id) //
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            Delegacion delegacion = await _context.Delegaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegacion == null)
            {
                return NotFound();
            }

            return View(delegacion);
        }

        /******************************************************CREAR DELEGACIONES*****************************************************/
        public IActionResult Create()
        {
            return View();
        }

        //**************************************************** POST: Delegacions/Create********************************//
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Delegacion delegacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(delegacion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Delegación con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(delegacion);

        }


        //************************************************* GET: DELEGACIONES EDITAR*******************************//

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            var delegacion = await _context.Delegaciones.FindAsync(id); //FinndAsync busca por clabe primaria
            if (delegacion == null)
            {
                return NotFound();
            }
            return View(delegacion);
        }

       //***********************************EDIT DELEGACIONES POST*******************************************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Delegacion delegacion)
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Delegación con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(delegacion);
        }


        //********************************************* GET: ELIMINAR DELEGACIONES*********************************************//

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            var delegacion = await _context.Delegaciones
                .FirstOrDefaultAsync(m => m.Id == id); //FirstOrDefaultAsync busca por todo la tabla
            if (delegacion == null)
            {
                return NotFound();
            }

            return View(delegacion);
        }


        // ********************************************************POST: ELIMINAR DELEGACIONES****************************************//


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
