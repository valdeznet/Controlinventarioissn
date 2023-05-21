using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Models
{
    public class AddEquipamientoImageViewModel
    {
        public int EquipamientoId { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }

    }
}
