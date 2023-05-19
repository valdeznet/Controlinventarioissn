using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Helpers;
using Controlinventarioissn.Migrations;
using Controlinventarioissn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controlinventarioissn.Controllers
{
    public class EquipamientoController :Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;

        public EquipamientoController(DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper)
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
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            CreateEquipamientoViewModel model = new()
            {
                Categories = await _combosHelper.GetComboCategoriesAsync(),
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

                equipamiento.EquipamientoCategories = new List<EquipamientoCategory>()
        {
            new EquipamientoCategory
            {
                Category = await _context.Categories.FindAsync(model.CategoryId)
            }
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
                    _context.Add(equipamiento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
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
            return View(model);
        }

    }

}
