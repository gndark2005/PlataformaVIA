namespace PlataformaVIAOAuth.WebServices.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioLoginInfo
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nit { get; set; }
        [StringLength(75)]
        public string RazonSocial { get; set; }
        public string CodigoPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public short DigitoVerificacion { get; set; }
        [StringLength(75)]
        public string FechaHoraUltimoIngreso { get; set; }
        //[Key, ForeignKey("Usuario")]
        //public string UserID { get; set; }
        //public virtual ApplicationUser Usuario { get; set; }
    }
}