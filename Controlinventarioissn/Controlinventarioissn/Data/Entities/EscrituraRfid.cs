﻿using System.ComponentModel.DataAnnotations;

namespace Controlinventarioissn.Data.Entities
{
    public class EscrituraRfid
    {
        public int Id { get; set; }//rfid se va a comvertir en una tabla en la Base de Datos

        [Display(Name = "Escritura Rfid")] //asi lo veo el  usuario
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]      //caracteres para almacenar del numero rfid, es necesario si no en la Base de datos se nos pone N charp+
        [Required(ErrorMessage = "El campo {0} es obligatorio")]           //que el no me deja poner un deposito sin nombre
        public string Name { get; set; }
    }
}
