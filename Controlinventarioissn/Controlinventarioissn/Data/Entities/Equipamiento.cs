using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Data.Entities
{
    public class Equipamiento
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        [Column(TypeName = "entero(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "NumeroRfid")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
             public int NumeroRfid { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Stock { get; set; }

        public ICollection<EquipamientoCategory> EquipamientoCategories { get; set; }

        [Display(Name = "Categorías")]
        public int CategoriesNumber => EquipamientoCategories == null ? 0 : EquipamientoCategories.Count;

        public ICollection<EquipamientoDeposito> EquipamientoDepositos { get; set; }

        [Display(Name = "Depositos")]
        public int DepositosNumber => EquipamientoDepositos == null ? 0 : EquipamientoDepositos.Count;

        public ICollection<EquipamientoImage> EquipamientoImages { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => EquipamientoImages == null ? 0 : EquipamientoImages.Count;

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => EquipamientoImages == null || EquipamientoImages.Count == 0
            ? $"https://localhost:7176/images/noimage.png"
            : EquipamientoImages.FirstOrDefault().ImageFullPath;

    }
}
