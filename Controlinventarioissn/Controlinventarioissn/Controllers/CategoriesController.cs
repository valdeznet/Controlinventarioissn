using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controlinventarioissn.Controllers
{
    public class CategoriesController : Controller //con esto tenemos acceso a la base de datos
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());                       
        }
        // ..........................................................GET: Delegacions/Create..............................................................//
        public IActionResult Create()
        {
            return View();
        }

        //**************************************************** POST: Delegacions/Create********************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Categoría con el mismo nombre.");
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
            return View(category);

        }
        //************************************************* GET: Delegacions/Edit/5*******************************//

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories.FindAsync(id); //FinndAsync busca por clabe primaria
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //***********************************EDIT POST*******************************************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Categoría con el mismo nombre.");
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
            return View(category);
        }

        //*........................................GET: Delegacions/Details/5..............................................................//
        public async Task<IActionResult> Details(int? id) //
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //********************************************* GET: Delegacions/Delete/5*********************************************//

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id); //FirstOrDefaultAsync busca por todo la tabla
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // ********************************************************POST: Delegacions/Delete/5****************************************//


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DataContext.Categorías'  is null.");
            }
            Category category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoríaExists(int id)
        {
            return (_context.Delegaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
