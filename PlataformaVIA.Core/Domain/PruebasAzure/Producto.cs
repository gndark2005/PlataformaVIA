namespace PlataformaVIA.Core.Domain.PruebasAzure
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede contener una longitud de {1} caracteres.")]
        [Index("Producto_Descripcion_Index", IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DisplayFormat(DataFormatString ="{0:c2}",ApplyFormatInEditMode =false)]
        public decimal Precio { get; set; }

        [Display(Name = "Está Activo")]
        public bool Activo { get; set; }

        [DataType(DataType.MultilineText)]
        public bool Comentarios { get; set; }

        [JsonIgnore]
        public virtual Categoria Categoria{ get; set; }
    }
}
