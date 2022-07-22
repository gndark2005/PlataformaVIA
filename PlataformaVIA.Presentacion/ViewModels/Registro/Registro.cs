namespace PlataformaVIA.Presentacion.ViewModels.Registro
{
    using System.ComponentModel.DataAnnotations;

    public class Registro
    {
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage ="{0} es requerido")]
        [EmailAddress(ErrorMessage = "{0} no válido")]
        public string Email { get; set; }

        [Display(Name = "Confirmar Correo electrónico")]
        [Compare("Email", ErrorMessage = "El correo y la confirmación no coinciden")]
        [EmailAddress(ErrorMessage = "{0} no válido")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de largo", MinimumLength = 10)]
        public string NombreCompleto { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de largo", MinimumLength = 7)]
        public string Telefono { get; set; }

        [Required( ErrorMessage ="{0} es requerida")]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de largo", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "{0} es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string PasswordConfirm { get; set; }

        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de largo", MinimumLength = 3)]
        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}