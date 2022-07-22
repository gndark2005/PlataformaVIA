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
    public class SolicitudBatchService : ISolicitudBatchService
    {
        public ISolicitudesBatchRepository SolicitudesBatchRepository { get; set; }

        public SolicitudBatchService(ISolicitudesBatchRepository SolicitudesBatchRepository)
        {
            this.SolicitudesBatchRepository = SolicitudesBatchRepository;
        }

        public ResponseEO<SolicitudesBatch> SolicitudEnvioReporte_ConsultarSolicitudesPendientes()
        {
            return this.SolicitudesBatchRepository.SolicitudEnvioReporte_ConsultarSolicitudesPendientes();
        }

        public int SolicitudEnvioReporte_MarcarSolicitudProcesada(SolicitudesBatch request)
        {
            return this.SolicitudesBatchRepository.SolicitudEnvioReporte_MarcarSolicitudProcesada(request);
        }

        public int SolicitudEnvioReporte_Agregar(SolicitudesBatch request)
        {
            return this.SolicitudesBatchRepository.SolicitudEnvioReporte_Agregar(request);
        }

    }

}

