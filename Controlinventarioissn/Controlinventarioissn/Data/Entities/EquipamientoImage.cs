using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Data.Entities
{
    public class EquipamientoImage
    {
        public int Id { get; set; }

        public Equipamiento Equipamiento { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7176/images/noimage.png"
            : $"https://controlinventarioissn.blob.core.windows.net/equipamientos/{ImageId}";

    }
}
