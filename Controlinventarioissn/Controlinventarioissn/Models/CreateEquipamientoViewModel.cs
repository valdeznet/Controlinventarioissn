using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Models
{
    public class CreateEquipamientoViewModel :EditEquipamientoViewModel
    {
        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Deposito")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Deposito.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DepositoId { get; set; }
        public IEnumerable<SelectListItem> Depositos { get; set; }

        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }

    }
}
