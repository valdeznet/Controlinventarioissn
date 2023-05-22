using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Helpers;
using Controlinventarioissn.Migrations;
using Controlinventarioissn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Controlinventarioissn.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EquipamientoController : Controller
    {
        private readonly Data.DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;

        public EquipamientoController(Data.DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipamientos
                .Include(e => e.EquipamientoImages)
                .Include(e => e.EquipamientoCategories)
                .ThenInclude(ec => ec.Category)
                .Include(e => e.EquipamientoDepositos)
                .ThenInclude(ec => ec.Deposito)
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            CreateEquipamientoViewModel model = new()
            {
                Categories = await _combosHelper.GetComboCategoriesAsync(),
                Depositos = await _combosHelper.GetComboDepositosAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEquipamientoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "equipamientos");
                }

                Equipamiento equipamiento = new()
                {
                    Description = model.Description,
                    Name = model.Name,
                    NumeroRfid = model.NumeroRfid,
                    Stock = model.Stock,
                };

                equipamiento.EquipamientoCategories = new List<EquipamientoCategory>() //a mi colleccion de categorias, hagale una nueva lista
                //de equipamiento categorie
        {
            new EquipamientoCategory
            {
                Category = await _context.Categories.FindAsync(model.CategoryId) //en la categoria vas a buscar en la coleccion de categorias
            }//ese id de categoria que te vino en el modelo
        };
                equipamiento.EquipamientoDepositos = new List<EquipamientoDeposito>() //a mi colleccion de depositos, hagale una nueva lista
                //de equipamiento categorie
        {
            new EquipamientoDeposito
            {
                Deposito = await _context.Depositos.FindAsync(model.DepositoId) //en el depsosito vas a buscar en la coleccion de depositos
            }//ese id de deposito que te vino en el modelo
        };

                if (imageId != Guid.Empty)
                {
                    equipamiento.EquipamientoImages = new List<EquipamientoImage>()
            {
                new EquipamientoImage { ImageId = imageId }
            };
                }

                try
                {
                    _context.Add(equipamiento); //
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un equipamiento con el mismo nombre.");
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

            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            model.Depositos = await _combosHelper.GetComboDepositosAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento product = await _context.Equipamientos.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            EditEquipamientoViewModel model = new()
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                NumeroRfid = product.NumeroRfid,
                Stock = product.Stock,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEquipamientoViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Equipamiento product = await _context.Equipamientos.FindAsync(model.Id);
                product.Description = model.Description;
                product.Name = model.Name;
                product.NumeroRfid = model.NumeroRfid;
                product.Stock = model.Stock;
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un Equipamiento con el mismo nombre.");
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

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento equipamiento = await _context.Equipamientos
                .Include(e => e.EquipamientoImages)
                .Include(e => e.EquipamientoCategories)
                .ThenInclude(ec => ec.Category)
                .Include(e => e.EquipamientoDepositos)
                .ThenInclude(ed => ed.Deposito)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (equipamiento == null)
            {
                return NotFound();
            }

            return View(equipamiento);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento equipamiento = await _context.Equipamientos.FindAsync(id);
            if (equipamiento == null)
            {
                return NotFound();
            }

            AddEquipamientoImageViewModel model = new()
            {
                EquipamientoId = equipamiento.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddEquipamientoImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "equipamentos");
                }

                Equipamiento equipamiento = await _context.Equipamientos.FindAsync(model.EquipamientoId);
                EquipamientoImage equipamientoImage = new()
                {
                    Equipamiento = equipamiento,
                    ImageId = imageId,
                };

                try
                {
                    _context.Add(equipamientoImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = equipamiento.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EquipamientoImage equipamientoImage = await _context.EquipamientoImages
                .Include(ei => ei.Equipamiento)
                .FirstOrDefaultAsync(ei => ei.Id == id);
            if (equipamientoImage == null)
            {
                return NotFound();
            }

            await _blobHelper.DeleteBlobAsync(equipamientoImage.ImageId, "equipamentos");
            _context.EquipamientoImages.Remove(equipamientoImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = equipamientoImage.Equipamiento.Id });
        }

        public async Task<IActionResult> AddCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento equipamiento = await _context.Equipamientos.FindAsync(id);
            if (equipamiento == null)
            {
                return NotFound();
            }

            AddCategoryEquipamientoViewModel model = new()
            {
                EquipamientoId = equipamiento.Id,
                Categories = await _combosHelper.GetComboCategoriesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddCategoryEquipamientoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Equipamiento equipamiento = await _context.Equipamientos.FindAsync(model.EquipamientoId);
                EquipamientoCategory equipamientoCategory = new()
                {
                    Category = await _context.Categories.FindAsync(model.CategoryId),
                    Equipamiento = equipamiento,
                };

                try
                {
                    _context.Add(equipamientoCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = equipamiento.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return View(model);
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EquipamientoCategory equipamientoCategory = await _context.EquipamientoCategories
                .Include(ec => ec.Equipamiento)
                .FirstOrDefaultAsync(ec => ec.Id == id);
            if (equipamientoCategory == null)
            {
                return NotFound();
            }

            _context.EquipamientoCategories.Remove(equipamientoCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = equipamientoCategory.Equipamiento.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento equipamiento = await _context.Equipamientos
                .Include(e => e.EquipamientoCategories)
                .Include(e => e.EquipamientoDepositos)
                .Include(e => e.EquipamientoImages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (equipamiento == null)
            {
                return NotFound();
            }

            return View(equipamiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Equipamiento model)
        {
            Equipamiento equipamiento = await _context.Equipamientos
                .Include(e => e.EquipamientoImages)
                .Include(e => e.EquipamientoDepositos)
                .Include(e => e.EquipamientoCategories)
                .FirstOrDefaultAsync(p => p.Id == model.Id);


            _context.Equipamientos.Remove(equipamiento);
            await _context.SaveChangesAsync();

            foreach (EquipamientoImage equipamientoImage in equipamiento.EquipamientoImages)
            {
                await _blobHelper.DeleteBlobAsync(equipamientoImage.ImageId, "equipamientos");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddDeposito(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamiento equipamiento = await _context.Equipamientos.FindAsync(id);
            if (equipamiento == null)
            {
                return NotFound();
            }

            AddDepositoEquipamientoViewModel model = new()
            {
                EquipamientoId = equipamiento.Id,
                Depositos = await _combosHelper.GetComboDepositosAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDeposito(AddDepositoEquipamientoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Equipamiento equipamiento = await _context.Equipamientos.FindAsync(model.EquipamientoId);
                EquipamientoDeposito equipamientoDeposito = new()
                {
                    Deposito = await _context.Depositos.FindAsync(model.DepositoId),
                    Equipamiento = equipamiento,
                };

                try
                {
                    _context.Add(equipamientoDeposito);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = equipamiento.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Depositos = await _combosHelper.GetComboDepositosAsync();
            return View(model);
        }

        public async Task<IActionResult> DeleteDeposito(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EquipamientoDeposito equipamientoDeposito = await _context.EquipamientoDepositos
                .Include(ed => ed.Equipamiento)
                .FirstOrDefaultAsync(ed => ed.Id == id);
            if (equipamientoDeposito == null)
            {
                return NotFound();
            }

            _context.EquipamientoDepositos.Remove(equipamientoDeposito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = equipamientoDeposito.Equipamiento.Id });
        }
    }
}
