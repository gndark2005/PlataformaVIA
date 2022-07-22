using PlataformaVIA.Core.Domain.Parametro;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Services.Implementations
{
    public class ParametroService: IParametroService
    {
        private IParametroRepository _parametroRepository;

        public ParametroService(IParametroRepository parametroRepository)
        {
            this._parametroRepository = parametroRepository;
        }
       
        public Parametro BuscarParametro(int codUsuario, string keyParametro)
        {
            return _parametroRepository.BuscarParametro(codUsuario, keyParametro);
        }
    }
}
