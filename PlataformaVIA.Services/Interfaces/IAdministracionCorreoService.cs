
using System.Collections.Generic;
using PlataformaVIA.Core.Domain;

namespace PlataformaVIA.Services.Interfaces
{
    public interface IAdministracionCorreoService
    {
        ResponseEO<AdministracionCorreo> GetCorreos();
        void SetEstadoCorreos(List<AdministracionCorreo> objCorreos);
        bool SendEmail(ResponseIndividualEO<AdministracionCorreo> correoNuevo);
        int AddCorreoPendiente(AdministracionCorreo objCorreo);
    }
}
