namespace PlataformaVIA.Identity.Models
{
    using PlataformaVIA.Core.Domain.Seguridad;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class UsuarioViewModel : Usuario
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud del campo {0} debe estar entre los  {1} y {2} caracteres", MinimumLength = 6)]
        public string Contrasena { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Compare("Contrasena", ErrorMessage = "La contraseña y la confirmación no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarContrasena { get; set; }
    }
}