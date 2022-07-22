namespace PlataformaVIA.Presentacion.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RazonSocial", Schema = "RedDistribucion")]
    public class RazonSocial
    {
        [Key]
        public int ID_RAZONSOCIAL { get; set; }
        public long NIT { get; set; }
        public string NOMBRERAZONSOCIAL { get; set; }
        
        public byte DIGITOVERIFICACION { get; set; }


        //public virtual ICollection<UsuarioInfo> UsuariosPuntoDeVenta { get; set; }
    }
}