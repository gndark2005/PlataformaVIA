namespace PlataformaVIA.Presentacion.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])).*", ErrorMessage = "La {0} debe tener al menos una mayúscula, una minuscula, numeros y caracteres especiales")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caractéres de largo.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="La {0} es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])).*", ErrorMessage = "La {0} debe tener al menos una mayúscula, una minuscula, numeros y caracteres especiales")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caractéres de largo.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeTerminalPasswordViewModel {
        [Display(Name ="Contraseña de terminal actual")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "La {0} debe ser numérica")]
        [StringLength(4, ErrorMessage = "La {0} debe tener {2} de largo", MinimumLength = 4)]
        public string OldTerminalPassword { get; set; }
        [Display(Name = "Contraseña de terminal nueva")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "La {0} debe ser numérica")]
        [StringLength(4, ErrorMessage = "La {0} debe tener {2} dígitos de largo", MinimumLength = 4)]
        public string NewTerminalPassword { get; set; }
        [Display(Name = "Confirmación Contraseña de terminal")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [Compare("NewTerminalPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string ConfirmTerminalPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}