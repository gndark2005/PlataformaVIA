using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class AdministracionCorreoRepository : AData<AdministracionCorreo>, IAdministracionCorreoRepository
    {

        /// <summary>
        /// Metodo que consulta correos pendientes por enviar.
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public ResponseEO<AdministracionCorreo> GetCorreos()
        {

            ResponseEO<AdministracionCorreo> ObjResponse = new ResponseEO<AdministracionCorreo>();

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ConsultarCorreosPendientes";

                    SqlParameter outputIdParam = new SqlParameter("@totalregistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    ObjResponse.Entidades = this.ToList<AdministracionCorreo>(command);

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        ObjResponse.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
                    }
                    else
                    {
                        ObjResponse.TotalRegistros = 0;
                    }
                }
            }

            return ObjResponse;
        }


        /// <summary>
        /// Metodo que actualiza el estado de los correos, en caso de tener error registra la excepción generada.
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public void SetEstadoCorreos(List<AdministracionCorreo> ObjCorreos)
        {
            DataTable dtCorreo = new DataTable();
            dtCorreo.Columns.Add("ID_CORREO");
            dtCorreo.Columns.Add("ID_ESTADO");
            dtCorreo.Columns.Add("RESPUESTA");

            foreach (var item in ObjCorreos)
            {
                DataRow dr = dtCorreo.NewRow();
                dr["ID_CORREO"] = item.ID_CORREO;
                dr["ID_ESTADO"] = item.ID_ESTADO;
                dr["RESPUESTA"] = item.RESPUESTA;

                dtCorreo.Rows.Add(dr);
            }

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_ActualizarEstadoCorreos";
                    command.Parameters.Add(command.CreateParameter("@RespuestaCorreo", dtCorreo));

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Metodo que actualiza el estado de los correos, en caso de tener error registra la excepción generada.
        /// Autor: Jeisson Juanias
        /// </summary>
        /// <returns></returns>
        public int AddCorreoPendiente(AdministracionCorreo ObjCorreo)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ViaBaloto_InsertarCorreos";

                    command.Parameters.Add(command.CreateParameter("@REQUIERE_TOKEN", ObjCorreo.REQUIERE_TOKEN));
                    if (!String.IsNullOrEmpty(ObjCorreo.PATH_STORAGE)) command.Parameters.Add(command.CreateParameter("@PATH_STORAGE", ObjCorreo.PATH_STORAGE));
                    command.Parameters.Add(command.CreateParameter("@TITULO", ObjCorreo.TITULO));
                    command.Parameters.Add(command.CreateParameter("@ASUNTO", ObjCorreo.ASUNTO));
                    command.Parameters.Add(command.CreateParameter("@MENSAJE", ObjCorreo.MENSAJE));
                    command.Parameters.Add(command.CreateParameter("@DESTINATARIOS", ObjCorreo.DESTINATARIOS));
                    command.Parameters.Add(command.CreateParameter("@COPIA_DESTINATARIOS", ObjCorreo.COPIA_DESTINATARIOS));
                    command.Parameters.Add(command.CreateParameter("@PATH_ADJUNTO", ObjCorreo.PATH_ADJUNTO));

                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool SendEmail(ResponseIndividualEO<AdministracionCorreo> correoNuevo) {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "AdministracionCorreo_EnviarCorreo";
                            command.Parameters.Add(command.CreateParameter("@Titulo", correoNuevo.Entidad.TITULO));
                            command.Parameters.Add(command.CreateParameter("@Asunto", correoNuevo.Entidad.ASUNTO));
                            command.Parameters.Add(command.CreateParameter("@Mensaje", correoNuevo.Entidad.MENSAJE));
                            command.Parameters.Add(command.CreateParameter("@Destinatarios", correoNuevo.Entidad.DESTINATARIOS));
                            command.Parameters.Add(command.CreateParameter("@CopiaDestinatarios", correoNuevo.Entidad.COPIA_DESTINATARIOS));
                            command.Parameters.Add(command.CreateParameter("@PathAdjunto", correoNuevo.Entidad.PATH_ADJUNTO));
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
