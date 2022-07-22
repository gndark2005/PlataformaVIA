namespace PlataformaVIA.Core.Domain.Seguridad
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede contener un máximo de {1} caracteres de longitud.")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede contener un máximo de {1} caracteres de longitud.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} solo puede contener un máximo de {1} caracteres de longitud.")]
        [Index("Usuario_Email_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} solo puede contener un máximo de {1} caracteres de longitud.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Display(Name = "Fecha Hora Último Ingreso")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaHoraUltimoIngreso { get; set; }



        //[Display(Name = "Image")]
        //public string ImagePath { get; set; }

        //[Display(Name = "Image")]
        //public string ImageFullPath
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(ImagePath))
        //        {
        //            return "noimage";
        //        }

        //        return string.Format(
        //            "http://landsapi1.azurewebsites.net/{0}",
        //            ImagePath.Substring(1));
        //    }
        //}

        [Display(Name = "User")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.Nombres, this.Apellidos);
            }
        }

        //public int Id_TipoUsuario { get; set; }

        //[JsonIgnore]
        //public virtual TipoUsuario TipoUsuario { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
