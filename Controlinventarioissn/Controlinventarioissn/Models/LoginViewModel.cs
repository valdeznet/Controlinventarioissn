﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Controlinventarioissn.Models
{
    public class LoginViewModel //
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]
        public string Username { get; set; }

        [DataType(DataType.Password)] //para que vea unos puntitos y no muestre la clave
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        public string Password { get; set; }

        [Display(Name = "Recordarme en este navegador")]
        public bool RememberMe { get; set; }

    }
}
