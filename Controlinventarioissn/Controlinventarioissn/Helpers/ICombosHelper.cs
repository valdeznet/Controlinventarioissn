using Microsoft.AspNetCore.Mvc.Rendering;
using Controlinventarioissn.Data.Entities;

namespace Controlinventarioissn.Helpers
{
	public interface ICombosHelper
	{
        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();

        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter);

        Task<IEnumerable<SelectListItem>> GetComboDelegacionesAsync();
        Task<IEnumerable<SelectListItem>> GetComboDepositosAsync();

        Task<IEnumerable<SelectListItem>> GetComboDepositosAsync(IEnumerable<Deposito> filter);
        Task<IEnumerable<SelectListItem>> GetComboSectorsAsync(int delegacionId);

    }
}
