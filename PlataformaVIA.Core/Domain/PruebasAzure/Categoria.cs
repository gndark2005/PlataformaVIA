namespace PlataformaVIA.Core.Domain.PruebasAzure
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Categoria
    {
        [Key]
        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50,ErrorMessage = "El campo {0} solo puede contener una longitud de {1} caracteres.")]
        [Index("Categoria_Descripcion_Index", IsUnique =true)]
        public string Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
