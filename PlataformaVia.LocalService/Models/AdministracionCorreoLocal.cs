//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlataformaVia.LocalService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdministracionCorreoLocal
    {
        public long ID_CORREO { get; set; }
        public string TITULO { get; set; }
        public string ASUNTO { get; set; }
        public string MENSAJE { get; set; }
        public string DESTINATARIOS { get; set; }
        public string COPIA_DESTINATARIOS { get; set; }
        public string PATH_ADJUNTO { get; set; }
        public long ID_ESTADO { get; set; }
        public string RESPUESTA { get; set; }
    }
}
