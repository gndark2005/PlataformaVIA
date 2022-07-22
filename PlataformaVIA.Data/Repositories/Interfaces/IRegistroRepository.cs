using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface IRegistroRepository
    {
        Registro RegistrarEvento(Registro objRegistro);
    }
}
