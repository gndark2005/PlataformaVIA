namespace PlataformaVIA.Core.Domain.Seguridad
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TipoUsuario
    {
        [Key]
        public int Id_TipoUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede contener un máximo de {1} caracteres de longitud.")]
        [Index("TipoUsuario_Nombre_Index", IsUnique = true)]
        public string Nombre { get; set; }

        [JsonIgnore]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
