using System;
using System.Data;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class NotificacionesRepository : AData<Notificaciones> , INotificacionesRepository
    {
        /// <summary>
        /// Metodo que consulta Notificaciones pendientes por enviar.
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public ResponseEO<Notificaciones> GetNotificaciones(int idUsuario)
        {

            ResponseEO<Notificaciones> ObjResponse = new ResponseEO<Notificaciones>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Notificacion_ConsultarNotificaciones";
                    command.Parameters.Add(command.CreateParameter("@IdUsuario", idUsuario));

                    ObjResponse.Entidades = this.ToList<Notificaciones>(command);
                }
            }

            return ObjResponse;
        }

        /// <summary>
        /// Metodo que realiza la creacion del token por usuario .
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public int CrearTokenPorUsuario(Notificaciones request)
        {
            int valor;

            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Notificacion_CrearTokenPorUsuario";
                        command.Parameters.Add(command.CreateParameter("@CODUSUARIOINFO", request.ID_NOTIFICACIONUSUARIOINFO));
                        command.Parameters.Add(command.CreateParameter("@TOKEN", request.TOKENNOTIFICADO));
                        command.Parameters.Add(command.CreateParameter("@CODTIPOTOKENNOTIFICACION", request.TipoTokenNotificacion));

                        valor = command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) {
                throw ex;
            }
        
            return valor;
        }

        /// <summary>
        /// Metodo que realiza la creacion de Notificaciones.
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public ResponseEO<Notificaciones> CrearNotificacion(Notificaciones request)
        {
            ResponseEO<Notificaciones> ObjResponse = new ResponseEO<Notificaciones>();

            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Notificacion_CrearNotificaciones";
                        command.Parameters.Add(command.CreateParameter("@TITULO", request.TITULO));
                        command.Parameters.Add(command.CreateParameter("@MENSAJE", request.MENSAJE));
                        command.Parameters.Add(command.CreateParameter("@URL", request.URL));
                        command.Parameters.Add(command.CreateParameter("@CODTIPOINGRESONOTIFICACION", request.TipoIngresoNotificacion));
                        command.Parameters.Add(command.CreateParameter("@AGENTENOTIFICACION", request.AGENTENOTIFICACION));

                        ObjResponse.Entidades = this.ToList<Notificaciones>(command);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ObjResponse;
        }


        /// <summary>
        /// Metodo que actualiza la notificacion 
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public int ActualizarNotificacion(Notificaciones request)
        {
            int valor;

            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Notificacion_MarcarLeida";
                        command.Parameters.Add(command.CreateParameter("@ID_NOTIFICACIONUSUARIOINFO", request.ID_NOTIFICACIONUSUARIOINFO));

                        valor = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return valor;
        }
    }
}
