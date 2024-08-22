using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace proyecto_javier.Models
{
    public class ClientePS
    {
        public int Codigo { get; set; }

        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 100 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los nombres deben tener entre 2 y 100 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El número de teléfono debe tener 10 dígitos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener solo números")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres")]
        public string Direccion { get; set; }

        [Required]
        public Boolean Estado { get; set; }

        [Range(0.00, 10000000, ErrorMessage = "El saldo debe estar entre 0 y 100,000")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Saldo { get; set; } = 0;


    }
}
