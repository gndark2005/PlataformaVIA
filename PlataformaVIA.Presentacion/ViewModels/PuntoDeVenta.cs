namespace PlataformaVIA.Presentacion.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PuntodeVenta", Schema = "RedDistribucion")]
    public class PuntoDeVenta : Core.Domain.PuntoDeVenta.PuntodeVenta
    {
        [ForeignKey("UsuarioPuntoDeVenta")]
        public virtual IEnumerable<UsuarioPuntoDeVenta> UsuariosPuntoDeVenta { get; set; }
    }
}