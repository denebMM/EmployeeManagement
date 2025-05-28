using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$",
              ErrorMessage = "Solo se permiten letras, números y espacios")]
        public string Name { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El correo debe tener entre 5 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
          ErrorMessage = "Formato inválido. Use: usuario@dominio.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Área")]
        [Required(ErrorMessage = "El área es obligatoria")]
        //[RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Solo se permiten letras, números y espacios")]
        
        public string Area { get; set; }

        [Display(Name = "Fecha de Contratación")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;
    }
}