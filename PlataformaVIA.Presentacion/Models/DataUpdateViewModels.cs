namespace PlataformaVIA.Presentacion.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DataUpdateViewModels
    {
        [Required(ErrorMessage = "El Correo electrónico es requerido")]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "El {0} no es válido")]
        public string EmailAlterno { get; set; }

        [Display(Name = "Celular")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} No cumple con la longitud")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} debe ser numerico")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Celular { get; set; }
    }
}