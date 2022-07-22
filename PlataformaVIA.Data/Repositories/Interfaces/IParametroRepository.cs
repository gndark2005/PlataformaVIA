using PlataformaVIA.Core.Domain.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface IParametroRepository
    {
        Parametro BuscarParametro(int codUsuario, string keyParametro);
    }
}
