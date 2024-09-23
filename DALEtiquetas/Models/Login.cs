using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALEtiquetas.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Por favor escriba el usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Por favor escriba el password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "la contraseña debe tener almenos 8 caracteres", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "La contraseña debe contener: Mínimo 8 caracteres, al menos 1 letra en mayúsculas, 1 letra en minúsculas, 1 número y 1 carácter especial.")]
        public string Password { get; set; }

        public string nomusuario { get; set; }

    }
}
