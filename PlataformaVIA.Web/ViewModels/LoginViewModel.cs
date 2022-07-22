using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaVIA.Web.ViewModels
{
    public class LoginViewModel
    {
        public bool CuentaMaestra { get; set; }

        public string Nit { get; set; }

        public string Password { get; set; }

        public bool Recordarme { get; set; }

        public bool MostrarCaptcha { get; set; }
    }
}