namespace PlataformaVIA.Presentacion.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Correo electrónico es requerido")]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "El Correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="El {0} es requerido")]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage ="La {0} es requerida")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])).*", ErrorMessage = "La {0} debe tener al menos una mayúscula, una minuscula, numeros y caracteres especiales")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caractéres de largo.", MinimumLength = 10)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="No es un {0} valido")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
