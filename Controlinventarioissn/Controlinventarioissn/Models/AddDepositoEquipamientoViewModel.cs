using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Models
{
    public class AddDepositoEquipamientoViewModel
    {
        public int EquipamientoId { get; set; }

        [Display(Name = "Deposito")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Deposito.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DepositoId { get; set; }

        public IEnumerable<SelectListItem> Depositos { get; set; }

    }
}
