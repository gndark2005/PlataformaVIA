using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;

namespace PlataformaVIA.Services.Implementations
{
    public class RegistroService : IRegistroService
    {
        public IRegistroRepository RegistroRepository { get; }

        public RegistroService(IRegistroRepository RegistroService)
        {
            this.RegistroRepository = RegistroService;
        }

        public Registro RegistrarEvento(Registro objRegistro)
        {
            return this.RegistroRepository.RegistrarEvento(objRegistro);
        }
    }
}
