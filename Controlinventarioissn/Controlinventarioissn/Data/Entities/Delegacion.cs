using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Controlinventarioissn.Data.Entities
{
    public class Delegacion
    {
        public int Id { get; set; }//Delegacion se va a comvertir en una tabla en la Base de Datos

        [Display(Name = "Delegación")] //asi lo veo el  usuario
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]      //caracteres para almacenar la delegacion, es necesario si no en la Base de datos se nos pone N charp+
        [Required(ErrorMessage = "El campo {0} es obligatorio")]           //que el no me deja poner un pais sin nombre
        public string Name { get; set; }

        public ICollection<Sector> Sectors { get; set; } //coleccion de sectores

        [Display(Name = "Sectores/Áreas")]

        public int SectorsNumber => Sectors == null ? 0 : Sectors.Count; //SI el numero de sector es igual a nulo me vas a devolver que el numero de sectores es cero 
        //propiedad de lectura que me de numero de sectores que tiene una delegacion
    }
}
