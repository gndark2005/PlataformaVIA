namespace PlataformaVIAOAuth.WebServices.ViewModels
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIAOAuth.WebServices.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ Table("UsuarioInfo", Schema ="Seguridad")]
    public class UsuarioInfo
    {
        [Key]
        public int ID_USUARIOINFO { get; set; }
        public string CODASPNETUSER { get; set; }
        public short CODPAIS { get; set; }
        //public short CODROLUSUARIO { get; set; }
        public int? CODVENDEDOR { get; set; }
        public int? CODRAZONSOCIAL { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public string EMAIL { get; set; }
        public bool? EMAILCONFIRMADO { get; set; }
        public DateTime? FECHAHORACONFIRMACIONMAIL { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public bool ACTIVO { get; set; }
        public DateTime? FECHAHORACREACION { get; set; }
        public DateTime? FECHAHORAULTIMOINGRESO { get; set; }
        public DateTime? FECHAHORAINGRESO { get; set; }

        [ForeignKey("CODASPNETUSER")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //[ForeignKey("CODRAZONSOCIAL")]
        //public virtual RazonSocial RazonSocial { get; set; }

        //[ForeignKey("CODPAIS")]
        //public virtual IEnumerable<ViewModels.Pais> Paises { get; set; }

        //[ForeignKey("CODROLUSUARIO")]
        //public virtual IEnumerable<ViewModels.RolUsuario> Roles { get; set; }


        //[ForeignKey("UsuarioPuntoDeVenta")]
        // public virtual IEnumerable<UsuarioPuntoDeVenta> UsuariosPuntoDeVenta { get; set; }

    }
}