namespace PlataformaVIA.Presentacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("PasswordHistory", Schema = "Seguridad")]
    public class PasswordHistory
    {
        public PasswordHistory()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public Int64 ID_PASSWORDHISTORY { set; get; }

        public DateTime CreatedDate { get; set; }

        //[Key, Column(Order = 1)]
        public string PasswordHash { get; set; }

        //[Key, Column(Order = 0)]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}