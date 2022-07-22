using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface ISolicitudesBatchRepository
    {
        ResponseEO<SolicitudesBatch> SolicitudEnvioReporte_ConsultarSolicitudesPendientes();
        int SolicitudEnvioReporte_MarcarSolicitudProcesada(SolicitudesBatch request);
        int SolicitudEnvioReporte_Agregar(SolicitudesBatch request);
    }
}
