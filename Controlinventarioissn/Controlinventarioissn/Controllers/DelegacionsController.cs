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
using Controlinventarioissn.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

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
                          View(await _context.Delegaciones
                          .Include(d => d.Sectors)
                          .ToListAsync()) :
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
                .Include(d => d.Sectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegacion == null)
            {
                return NotFound();
            }

            return View(delegacion);
        }


        /******************************************************GET CREAR DELEGACIONES*****************************************************/
        public IActionResult Create()
        {
            Delegacion delegacion = new() { Sectors = new List<Sector>() };
            return View(delegacion);
        }

        //**************************************************** POST: DELEGACIONES/Create********************************//
   
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

            Delegacion delegacion = await _context.Delegaciones
                .Include(d => d.Sectors)
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

        /******************************************************ADD SECTOR*****************************************************/
        public async Task <IActionResult> AddSector(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delegacion delegacion = await _context.Delegaciones.FindAsync(id);
            if (delegacion == null)
            {
                return NotFound();
            }

            SectorViewModel model = new()
            {
                DelegacionId = delegacion.Id,
            };

            return View(model);
        }

        //**************************************************** POST: SECTOR CREATE********************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSector(SectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Sector sector = new()
                    {
                        Delegacion = await _context.Delegaciones.FindAsync(model.DelegacionId),
                        Name = model.Name,
                    };
                    _context.Add(sector);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.DelegacionId});
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Sector/Área con el mismo nombre.");
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
            return View(model);

        }


        //************************************************* GET: SECTORES EDITAR*******************************//

        public async Task<IActionResult> EditSector(int? id)
        {
            if (id == null || _context.Delegaciones == null)
            {
                return NotFound();
            }

            Sector sector = await _context.Sectors
                .Include(s => s.Delegacion) //tambien me traiga la Delegacion
                .FirstOrDefaultAsync(s => s.Id == id); //me traiga el sector
            if (sector == null)
            {
                return NotFound();
            }

            SectorViewModel model = new() //elmodel es que le 
            {
                DelegacionId = sector.Delegacion.Id, //
                Id = sector.Id,
                Name = sector.Name,
            };

            return View(model);
        }

        //***********************************EDITAR SECTORES POST*******************************************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSector(int id, SectorViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   Sector sector = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                    };
                    _context.Update(sector);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.DelegacionId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Sector con el mismo nombre en esta delegación.");
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
            return View(model);    
       }

        //******************************************ELIMINAR SECTOR*************************************++//
        public async Task<IActionResult> DeleteSector(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sector sector = await _context.Sectors
                .Include(s => s.Delegacion) //es Country para que pueda devolver
                .FirstOrDefaultAsync(s => s.Id == id);  //otra forma de buscar objeto delegacion, este busca por cualquier campo
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        //*********************************** POST: SECTOR ELIMINAR ****************************************+//

        [HttpPost, ActionName("DeleteSector")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSectorConfirmed(int id)
        {
            Sector sector = await _context.Sectors
                .Include(s => s.Delegacion) //es Delegacion para que pueda devolver
                .FirstOrDefaultAsync(s => s.Id == id);
            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = sector.Delegacion.Id });
        }
    }

}
