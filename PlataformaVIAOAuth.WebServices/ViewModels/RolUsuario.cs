using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlataformaVIAOAuth.WebServices.ViewModels
{
    [Table("RolUsuario", Schema = "Seguridad")]
    public class RolUsuario
    {
        public int ID_ROLUSUARIO { get; set; }
        public string NOMBRE { get; set; }
    }
}