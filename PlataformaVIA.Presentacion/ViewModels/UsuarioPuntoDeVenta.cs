using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlataformaVIA.Presentacion.ViewModels
{
    [Table("UsuarioPuntoDeVenta", Schema = "Seguridad")]
    public class UsuarioPuntoDeVenta
    {
        [Key]
        public int ID_USUARIOPUNTODEVENTA { get; set; }        
        public int? CODUSUARIOINFO { get; set; }
        public int? CODPUNTODEVENTA { get; set; }


        [ForeignKey("CODUSUARIOINFO")]
        public virtual UsuarioInfo Usuarios { get; set; }

        [ForeignKey("CODPUNTODEVENTA")]
        public virtual PuntoDeVenta PuntosDeVenta { get; set; }
    }
}