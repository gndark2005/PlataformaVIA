namespace PlataformaVIA.Core.Domain.RepresentanteLegal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Colocador
    {
        public Int64 Id { get; set; }
        public string Numero { get; set; }
        
        public string TipoIdentificacion { get; set; }

        [Display(Name = "NIT")]
        public Int64 NIT { get; set; }

        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Display(Name = "Tipo de Identificación")]
        [Required(ErrorMessage = "Debe seleccionar {0}")]
        public int CodTipoIdentificacion { get; set; }


        [Display(Name = "Número de Identificación")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} No cumple con la longitud")]
        public string NumeroIdentificacion { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} No cumple con la longitud")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} No cumple con la longitud")]
        public string Apellidos { get; set; }

        public string NombreCompleto
        {
            get { return Nombres + " " + Apellidos; }
        }

        [Display(Name = "Ciudad de Expedición de Cédula")]
        public string CiudadExpedicionCedula { get; set; }

        [Display(Name = "Ciudad de Expedición de Cédula")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar {0}")]
        public int CodCiudadExpedicionCedula { get; set; }

        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Display(Name = "Género")]
        public int CodGenero { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.DateTime, ErrorMessage ="{0} formato no valido")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        public DateTime FechaNacimiento { get; set; }
            
        [Display(Name = "Ciudad de Nacimiento")]
        public string CiudadNacimiento { get; set; }

        [Display(Name = "Ciudad de Nacimiento")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar {0}")]
        public int CodCiudadNacimiento { get; set; }

        [Display(Name = "Dirección del Vendedor")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "{0} No cumple con la longitud")]
        public string DireccionVendedor { get; set; }

        [Display(Name = "Ciudad del Vendedor")]
        public string CiudadVendedor { get; set; }

        [Display(Name = "Ciudad del Vendedor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar {0}")]
        public int CodCiudadVendedor { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} debe ser numerico")]
        public string Telefono { get; set; }

        [Display(Name = "Celular")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} No cumple con la longitud")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} debe ser numerico")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public string Celular { get; set; }

        [Display(Name = "Tipo de sangre")]
        public string TipoSangre { get; set; }

        [Display(Name = "Tipo de sangre")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int CodTipoSangre { get; set; }

        [Display(Name = "Tipo de colocador")]
        public string TipoColocador { get; set; }

        [Display(Name = "Tipo de colocador")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        public int CodTipoColocador { get; set; }

        [Display(Name = "Dirección de correo electrónico")]
        [Required(ErrorMessage = "Debe diligenciar {0}")]
        [EmailAddress(ErrorMessage = "{0} no valido")]
        public string CorreoElectronico { get; set; }

        public bool UsuarioAsignado { get; set; }
        public int IdUsuarioEdit { get; set; }

        [Display(Name = "Otorgar acceso a todos los puntos de venta asociados a la Razón Social")]
        public bool? OtorgarAccesoGlobal { get; set; }

        public string ClaveTerminal { get; set; }
        public bool Habilitado { get; set; }

        public int CodigoPuntoDeVenta { get; set; }
        public int Sincronizar { get; set; }

        public string TipoVendedor { get; set; }

        List<PuntosVentaAcceso> PuntosdeventaConAcceso { get; set; }
    }
}
