using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Controlinventarioissn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Equipamiento>? equipamientos = await _context.Equipamientos
                .Include(p => p.EquipamientoImages)
                .Include(p => p.EquipamientoCategories)
                .OrderBy(p => p.Description)
                .ToListAsync();
            List<EquipamientosHomeViewModel> equipamientosHome = new() { new EquipamientosHomeViewModel() };
            int i = 1;
            foreach (Equipamiento? equipamiento in equipamientos)
            {
                if (i == 1)
                {
                    equipamientosHome.LastOrDefault().Equipamiento1 = equipamiento;
                }
                if (i == 2)
                {
                    equipamientosHome.LastOrDefault().Equipamiento2 = equipamiento;
                }
                if (i == 3)
                {
                    equipamientosHome.LastOrDefault().Equipamiento3 = equipamiento;
                }
                if (i == 4)
                {
                    equipamientosHome.LastOrDefault().Equipamiento4 = equipamiento;
                    equipamientosHome.Add(new EquipamientosHomeViewModel());
                    i = 0;
                }
                i++;
            }

            return View(equipamientosHome);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}