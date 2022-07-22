using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion.ViewModels.Certificados
{
    public class GenerarCertificadosViewModel
    {
        [Display(Name = "Certificado de Colaboración")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        public int codTipoCertificado { get; set; }

        [Display(Name = "Mes")]
        [Required(ErrorMessage = "Por favor seleccionar {0}")]
        public string mesCertificado { get; set; }
        public SelectList TiposDeCertificadoList { get; set; }
        
    }
}