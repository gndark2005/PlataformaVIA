using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class SolicitudesBatchRepository : AData<SolicitudesBatch>, ISolicitudesBatchRepository
    {
        /// <summary>
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public ResponseEO<SolicitudesBatch> SolicitudEnvioReporte_ConsultarSolicitudesPendientes()
        {
            ResponseEO<SolicitudesBatch> ObjResponse = new ResponseEO<SolicitudesBatch>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SolicitudEnvioReporte_ConsultarSolicitudesPendientes";

                    ObjResponse.Entidades = this.ToList<SolicitudesBatch>(command);
                }
            }

            return ObjResponse;
        }

        /// <summary>
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public int SolicitudEnvioReporte_MarcarSolicitudProcesada(SolicitudesBatch request)
        {
            int result;

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SolicitudEnvioReporte_MarcarSolicitudProcesada";
                    command.Parameters.Add(command.CreateParameter("@ID_SOLICITUDENVIOREPORTE", request.ID_SOLICITUDENVIOREPORTE));

                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        /// <summary>
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public int SolicitudEnvioReporte_Agregar(SolicitudesBatch request)
        {
            int result;

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SolicitudEnvioReporte_Agregar";
                    command.Parameters.Add(command.CreateParameter("@Codusuario", request.CODUSUARIOINFOSOLICITUD));
                    command.Parameters.Add(command.CreateParameter("@CodTipoSolicitudEnvioReporte", request.CodTipoSolicitudEnvioReporte));
                    if (request.CodPuntoDeVenta > 0)
                        command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", request.CodPuntoDeVenta));
                    if (request.CodCadena > 0)
                        command.Parameters.Add(command.CreateParameter("@CodCadena", request.CodCadena));
                    command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", request.CodCicloFacturacion));

                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }
    }
}
