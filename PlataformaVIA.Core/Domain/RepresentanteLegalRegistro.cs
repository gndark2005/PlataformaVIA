using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain
{
    public class RepresentanteLegalRegistro
    {
        public string Nit { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Contraseña")]
        [StringLength(100, ErrorMessage = "La contraseña {0} debe contener mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        [DataType(DataType.Password)]
        [Compare("Contrasenia", ErrorMessage = "Los campos contraseña y confirmar contraseña no son iguales.")]
        [DisplayName("Confirmar contraseña")]
        public string ConfirmarContrasenia { get; set; }
        [DisplayName("Dirección de Correo Electrónico")]
        public string Email { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
        public int CodUSuario { get; set; }
    }
}
