using PlataformaVIA.Core.Domain.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Services.Interfaces
{
    public interface IParametroService
    {
        Parametro BuscarParametro(int codUsuario, string keyParametro);
    }
}
