using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;

namespace PlataformaVIA.Services.Interfaces
{
    public interface IRegistroService
    {
        Registro RegistrarEvento(Registro objRegistro);
    }
}
