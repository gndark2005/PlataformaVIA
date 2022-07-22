namespace PlataformaVIA.Core.Domain.Seguridad
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UsuarioInfo", Schema = "Seguridad")]
    public class UsuarioInfo
    {
        [Key]
        public int ID_USUARIOINFO { get; set; }
        [MaxLength(128)]
        public string CODASPNETUSER { get; set; }
        public short CODPAIS { get; set; }
        /*public short CODROLUSUARIO { get; set; }*/
        public int? CODVENDEDOR { get; set; }
        public int? CODRAZONSOCIAL { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public string EMAIL { get; set; }
        public bool? EMAILCONFIRMADO { get; set; }
        public bool? EMAILALTERNO { get; set; }
        public DateTime? FECHAHORACONFIRMACIONMAIL { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public bool ACTIVO { get; set; }
        public DateTime? FECHAHORACREACION { get; set; }
        public DateTime? FECHAHORAULTIMOINGRESO { get; set; }
        public DateTime? FECHAHORAINGRESO { get; set; }
        public string TOKEN { get; set; }
        public string ROLE { get; set; }
    }
}
