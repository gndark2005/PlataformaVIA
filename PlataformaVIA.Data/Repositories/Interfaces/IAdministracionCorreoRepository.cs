
using System;
using System.Collections.Generic;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.ViaBaloto;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface IAdministracionCorreoRepository
    {
        ResponseEO<AdministracionCorreo> GetCorreos();
        void SetEstadoCorreos(List<AdministracionCorreo> objCorreos);
        bool SendEmail(ResponseIndividualEO<AdministracionCorreo> correoNuevo);
        int AddCorreoPendiente(AdministracionCorreo objCorreo);

    }
}
