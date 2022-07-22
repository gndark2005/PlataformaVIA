using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Seguridad
{
    public class ValidacionToken
    {
        public bool EsValido { get; set; }
        public string Mensaje { get; set; }
    }
}
