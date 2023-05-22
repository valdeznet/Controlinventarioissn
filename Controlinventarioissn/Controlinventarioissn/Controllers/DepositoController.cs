using Controlinventarioissn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controlinventarioissn.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Controlinventarioissn.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepositoController : Controller
    {
        private readonly Data.DataContext _context;

        public DepositoController(Data.DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depositos.ToListAsync());
        }

        //*********************************************GET DEPOSITOS/Create******************************************//
        public IActionResult Create()
        {
            return View();
        }

        //**************************************************** POST: DEPOSITOS/Create********************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Deposito deposito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(deposito);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Deposito con el mismo nombre.");
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
            return View(deposito);
        }

        //************************************************* GET: EDITAR DEPOSITOS************************************//

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Depositos == null)
            {
                return NotFound();
            }

            Deposito deposito = await _context.Depositos.FindAsync(id); //FinndAsync busca por clave primaria
            if (deposito == null)
            {
                return NotFound();
            }
            return View(deposito);
        }

        //***********************************EDIT DEPOSITOS POST*******************************************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Deposito deposito)
        {
            if (id != deposito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deposito);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Deposito con el mismo nombre.");
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
            return View(deposito);
        }

        //************************************************GET: DETALLES DEPOSITOS************************************************//
        public async Task<IActionResult> Details(int? id) //
        {
            if (id == null || _context.Depositos == null)
            {
                return NotFound();
            }

            Deposito deposito = await _context.Depositos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposito == null)
            {
                return NotFound();
            }

            return View(deposito);
        }

        //********************************************* GET: ELIMINAR DEPOSITOS*********************************************//

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Depositos == null)
            {
                return NotFound();
            }

            Deposito deposito = await _context.Depositos
                .FirstOrDefaultAsync(m => m.Id == id); //FirstOrDefaultAsync busca por todo la tabla
            if (deposito == null)
            {
                return NotFound();
            }

            return View(deposito);
        }


        // ********************************************************POST: ELIMINAR DEPOSITOS****************************************//


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Depositos == null)
            {
                return Problem("Entity set 'DataContext.Depositos'  is null.");
            }
            Deposito deposito = await _context.Depositos.FindAsync(id);
            if (deposito != null)
            {
                _context.Depositos.Remove(deposito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositoExists(int id)
        {
            return (_context.Depositos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
