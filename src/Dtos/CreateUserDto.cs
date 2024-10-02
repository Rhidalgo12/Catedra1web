using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.Dtos
{
    public class CreateUserDto
    {
        public required string Rut { get; set; }

        [StringLength(100, MinimumLength =3)]
        public required string Nombre { get; set; }

         [EmailAddress]
        public required string Email { get; set; }

         [RegularExpression(@"Masculino|Femenino|Otro|Prefiero no decirlo")]
        public required string Genero { get; set; }

        public DateTime Fecha_nacimiento { get; set; }
    }
}