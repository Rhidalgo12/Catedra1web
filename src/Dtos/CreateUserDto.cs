using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El RUT es obligatorio")]
        public required string Rut { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [RegularExpression(@"^(Masculino|Femenino|Otro|Prefiero no decirlo)$", 
            ErrorMessage = "El género debe ser 'Masculino', 'Femenino', 'Otro' o 'Prefiero no decirlo'")]
        public required string Genero { get; set; }

        
        public DateOnly Fecha_nacimiento { get; set; }
    }
}