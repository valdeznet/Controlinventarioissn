using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Models
{
    public class EditEquipamientoViewModel
    {
            public int Id { get; set; }

            [Display(Name = "Nombre")]
            [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public string Name { get; set; }

            [Display(Name = "Descripción")]
            [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public string Description { get; set; }

       // Column(TypeName = "entero(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "NumeroRfid")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int NumeroRfid { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
            [Display(Name = "Inventario")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]
            public float Stock { get; set; }
        }
}

