using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;

namespace Controlinventarioissn.Helpers
{
	public class CombosHelper : ICombosHelper
	{
		private readonly DataContext _context;
		public CombosHelper(DataContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
		{
			List<SelectListItem> list = await _context.Categories.Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			})
				.OrderBy(c => c.Text)
				.ToListAsync();

			list.Insert(0, new SelectListItem { Text = "[Selecione una categoria...]", Value = "0" });
			return list;
		}

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter)
        {
            List<Category> categories = await _context.Categories.ToListAsync(); //tengo mi lista de categoria
            List<Category> categoriesFiltered = new (); //creamos otra lista de categoria que se llama categorias filtradas
            foreach (Category category in categories) //ahora usca en todas las categorias que estan en el filtro
            {
                if (!filter.Any(c => c.Id == category.Id))      //por cada categoria en la lista de categoria vamos a decirle 
                {                                               //(!FIlter , esto si NO existe en el Filtro --- si mi filtro me devuelve cualquiera, una categoria que tenga el c.Id igual al category.Id quiere decir que existe en el filtro
                    categoriesFiltered.Add(category); //de esa manera si no exite adicionamos la categoria
                }
            }

            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Selecione una categoria...]", Value = "0" });
            return list;
        } //ahora que ya esta el metodo nos toca llamar a este filtro, vamos al productcontroller



        public async Task<IEnumerable<SelectListItem>> GetComboDelegacionesAsync()
        {
            List<SelectListItem> list = await _context.Delegaciones.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Selecione una Delegación..]", Value = "0" });


            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboSectorsAsync(int delegacionId)
        {
            List<SelectListItem> list = await _context.Delegaciones.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Selecione una Delegación..]", Value = "0" });

            return list;
        }
	}
}
