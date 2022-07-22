using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.MailAdministrator.AppCode
{
    public class EnumEstado
    {
        public enum EstadoMensaje
        {
            Enviado = 1,
            Pendiente = 2,
            Error_Envio = 3
        };
    }
}
