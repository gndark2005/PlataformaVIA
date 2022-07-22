using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlataformaVIAOAuth.WebServices.ViewModels
{
    [Table("Pais", Schema = "RedDistribucion")]
    public class Pais
    {
        public int ID_PAIS { get; set; }
        public String NOMBRE { get; set; }
    }
}