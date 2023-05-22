using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Controlinventarioissn.Data;
using Controlinventarioissn.Data.Entities;

namespace Controlinventarioissn.Helpers
{
	public class CombosHelper : ICombosHelper
	{
		private readonly Data.DataContext _context;
		public CombosHelper(Data.DataContext context)
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
        } //ahora que ya esta el metodo nos toca llamar a este filtro, vamos al equipamientocontroller

//***************************************************************************************************************//
        public async Task<IEnumerable<SelectListItem>> GetComboDepositosAsync()
        {
            List<SelectListItem> list = await _context.Depositos.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Selecione un Deposito...]", Value = "0" });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboDepositosAsync(IEnumerable<Deposito> filter)
        {
            List<Deposito> depositos = await _context.Depositos.ToListAsync(); //tengo mi lista de deposito
            List<Deposito> depositosFiltered = new(); //creamos otra lista de deposito que se llama depositos filtradas
            foreach (Deposito deposito in depositos) //ahora busca en todas los depositos que estan en el filtro
            {
                if (!filter.Any(d => d.Id == deposito.Id))      //por cada deposito en la lista de depositos vamos a decirle 
                {                                               //(!FIlter , esto si NO existe en el Filtro --- si mi filtro me devuelve cualquiera, un depsoto que tenga el c.Id igual al category.Id quiere decir que existe en el filtro
                    depositosFiltered.Add(deposito); //de esa manera si no exite adicionamos la categoria
                }
            }

            List<SelectListItem> list = depositosFiltered.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            })
                .OrderBy(d => d.Text)
                .ToList();

            list.Insert(0, new SelectListItem { Text = "[Selecione un Deposito...]", Value = "0" });
            return list; //ahora que ya esta el metodo nos toca llamar a este filtro, vamos al Equipamientocontroller
        }

//************************************************************************************************************************************//
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
