using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Data.Entities
{
    public class Sector
    {
        public int Id { get; set; }//Sector se va a comvertir en una tabla en la Base de Datos

        [Display(Name = "Sector/Área")] //asi lo veo el  usuario
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]      //caracteres para almacenar la delegacion, es necesario si no en la Base de datos se nos pone N charp+
        [Required(ErrorMessage = "El campo {0} es obligatorio")]           //que el no me deja poner un pais sin nombre
        public string Name { get; set; }

        public Delegacion Delegacion { get; set; } //relacion de uno a varios

    }
}
