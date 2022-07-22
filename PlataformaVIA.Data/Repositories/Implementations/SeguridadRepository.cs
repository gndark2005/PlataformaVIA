namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain.Seguridad;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class SeguridadRepository : AData<Pregunta>, ISeguridadRepository
    {


        public ResponseEO<Pregunta> GetPreguntasDeSeguridad(string Nit)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                ResponseEO<Pregunta> preguntas = new ResponseEO<Pregunta>();
                preguntas.Entidades = new List<Pregunta>();
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_RegistroRepresentante_ObtenerPreguntas";
                    command.Parameters.Add(command.CreateParameter("@Nit", Nit));
                    try
                    {
                        var dr = command.ExecuteReader();
                        List<Pregunta> preguntasAux = new List<Pregunta>();
                        while (dr.Read())
                        {
                            List<Respuesta> respuestas = new List<Respuesta>();

                            respuestas.Add(new Respuesta { Valor = dr["RespuestaUno"].ToString() });
                            respuestas.Add(new Respuesta { Valor = dr["RespuestaDos"].ToString() });
                            respuestas.Add(new Respuesta { Valor = dr["RespuestaTres"].ToString() });
                            preguntasAux.Add(new Pregunta { Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString(), Respuestas = respuestas });

                        }
                        preguntas.Entidades = preguntasAux;
                    }
                    catch (SqlException exSql)
                    {
                        preguntas.Mensaje = new Message { Error = true, ErrorMessage = exSql.Message };
                    }
                    catch (Exception)
                    {
                        preguntas.Mensaje = new Message { Error = true, ErrorMessage = "Ha ocurrido un error, por favor contacte al administrador" };
                    }
                }
                return preguntas;
            }
        }

        public ResponseIndividualEO<ValidacionTokenResponse> ValidarTokenDeSeguridad(string Token)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                ResponseIndividualEO<ValidacionTokenResponse> validacionToken = new ResponseIndividualEO<ValidacionTokenResponse>();
                validacionToken.Entidad = new ValidacionTokenResponse();
                validacionToken.Entidad.Token = Token;
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_TokenRegistroRespuestaExitosa_ValidarToken";
                    command.Parameters.Add(command.CreateParameter("@TOKEN", Token));

                    SqlParameter outputValido = new SqlParameter("@VALIDO", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputValido);

                    SqlParameter outputCodRazonSocial = new SqlParameter("@CODRAZONSOCIAL", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputCodRazonSocial);

                    SqlParameter outputMensajeError = new SqlParameter("@MENSAJEERROR", SqlDbType.VarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputMensajeError);

                    try
                    {
                        var dr = command.ExecuteReader();
                        while (dr.Read()) { }

                        if (outputValido.Value != DBNull.Value)
                            validacionToken.Entidad.Valido = Convert.ToBoolean(outputValido.Value);

                        if (outputCodRazonSocial.Value != DBNull.Value)
                            validacionToken.Entidad.CodRazonSocial = Convert.ToInt32(outputCodRazonSocial.Value);

                        if (outputMensajeError.Value != DBNull.Value)
                            validacionToken.Entidad.MensajeError = Convert.ToString(outputMensajeError.Value);
                    }
                    catch (SqlException exSql)
                    {
                        validacionToken.Mensaje = new Message { Error = true, ErrorMessage = exSql.Message };
                    }
                    catch (Exception ex)
                    {
                        validacionToken.Mensaje = new Message { Error = true, ErrorMessage = ex.Message };
                    }
                }
                return validacionToken;
            }
        }

        public int ObtenerRazonSocialPorToken(string token)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                int codRazonSocial = 0;
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_RegistroRepresentante_ObtenerRazonSocialPorToken";
                    command.Parameters.Add(command.CreateParameter("@TOKEN", token));
                    codRazonSocial = Convert.ToInt32(command.ExecuteScalar());
                }
                return codRazonSocial;
            }
        }

        public ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> ValidarPreguntasSeguridad(ValidacionPreguntasSeguridad validacionPreguntasSeguridad)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> validacionPreguntasSeguridadResponse = new ResponseIndividualEO<ValidacionPreguntasSeguridadResponse>();
                validacionPreguntasSeguridadResponse.Entidad = new ValidacionPreguntasSeguridadResponse();
                List<Pregunta> preguntas = new List<Pregunta>();
                using (var command = context.CreateCommand())
                {
                    DataTable respuetasSeleccionadas = ConvertirPreguntasATabla(validacionPreguntasSeguridad.PreguntasSeguridad);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_RegistroRepresentante_ValidarPreguntas";
                    command.Parameters.Add(command.CreateParameter("@Nit", validacionPreguntasSeguridad.Nit));
                    command.Parameters.Add(command.CreateParameter("@Respuestas", respuetasSeleccionadas));
                    try
                    {

                        var dr = command.ExecuteReader();

                        // Tiene columna de mensaje
                        var filasMensaje = dr.GetSchemaTable().Select("ColumnName = 'Mensaje'");
                        var filasToken = dr.GetSchemaTable().Select("ColumnName = 'Token'");
                        if (filasMensaje != null && filasMensaje.Count() == 1)
                        {
                            while (dr.Read())
                            {
                                validacionPreguntasSeguridadResponse.Mensaje.Error = true;
                                validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = dr["Mensaje"].ToString();
                            }

                            validacionPreguntasSeguridadResponse.Entidad.Token = string.Empty;
                            validacionPreguntasSeguridadResponse.Entidad.UsuarioBloqueado = true;
                            validacionPreguntasSeguridadResponse.Entidad.Preguntas = null;
                        }
                        // Tiene columna de token, osea validación correcta
                        else if (filasToken != null & filasToken.Count() == 1)
                        {
                            while (dr.Read())
                            {
                                validacionPreguntasSeguridadResponse.Entidad.Token = dr["Token"].ToString();
                            }
                            validacionPreguntasSeguridadResponse.Entidad.ValidacionExitosa = true;
                            validacionPreguntasSeguridadResponse.Mensaje.Error = false;
                            validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = string.Empty;
                            validacionPreguntasSeguridadResponse.Entidad.Preguntas = null;
                        }
                        else
                        {
                            // algo pasó
                        }
                    }
                    catch (SqlException exSql)
                    {
                        validacionPreguntasSeguridadResponse.Mensaje.Error = true;
                        validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = exSql.Message;
                    }
                    catch (Exception ex)
                    {
                        validacionPreguntasSeguridadResponse.Mensaje.Error = true;
                        validacionPreguntasSeguridadResponse.Mensaje.ErrorMessage = ex.Message;
                    }
                }
                return validacionPreguntasSeguridadResponse;
            }
        }

        private DataTable ConvertirPreguntasATabla(IEnumerable<Pregunta> preguntas)
        {
            DataTable dtPreguntas = new DataTable();
            dtPreguntas.Columns.Add("CodPregunta");
            dtPreguntas.Columns.Add("Respuesta");

            foreach (Pregunta pregunta in preguntas)
            {
                DataRow dr = dtPreguntas.NewRow();
                dr["CodPregunta"] = pregunta.Id;
                dr["Respuesta"] = pregunta.RespuestaSeleccionada;
                dtPreguntas.Rows.Add(dr);
            }

            return dtPreguntas;
        }

        public UsuarioInfo GetUsuarioInfo(string Email)
        {
            UsuarioInfo user = new UsuarioInfo();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_GetUsuarioInfo";
                    command.Parameters.Add(command.CreateParameter("@EMAIL", Email));
                    var dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        user = new UsuarioInfo()
                        {
                            ID_USUARIOINFO = Convert.ToInt32(dr["ID_USUARIOINFO"]),
                            CODASPNETUSER = dr["CODASPNETUSER"].ToString(),
                            CODPAIS = string.IsNullOrEmpty(dr["CODPAIS"].ToString()) ? Convert.ToInt16("0") : Convert.ToInt16(dr["CODPAIS"]),
                            /*CODROLUSUARIO = string.IsNullOrEmpty(dr["CODROLUSUARIO"].ToString()) ? Convert.ToInt16("0") : Convert.ToInt16(dr["CODROLUSUARIO"]),*/
                            CODVENDEDOR = string.IsNullOrEmpty(dr["CODVENDEDOR"].ToString()) ? 0 : Convert.ToInt32(dr["CODVENDEDOR"]),
                            CODRAZONSOCIAL = string.IsNullOrEmpty(dr["CODRAZONSOCIAL"].ToString()) ? 0 : Convert.ToInt32(dr["CODRAZONSOCIAL"]),
                            NOMBREUSUARIO = dr["NOMBREUSUARIO"].ToString(),
                            EMAIL = dr["EMAIL"].ToString(),
                            EMAILCONFIRMADO = string.IsNullOrEmpty(dr["EMAILCONFIRMADO"].ToString()) ? false : Convert.ToBoolean(dr["EMAILCONFIRMADO"]),
                            FECHAHORACONFIRMACIONMAIL = string.IsNullOrEmpty(dr["FECHAHORACONFIRMACIONMAIL"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FECHAHORACONFIRMACIONMAIL"]),
                            CELULAR = dr["CELULAR"].ToString(),
                            TELEFONO = string.IsNullOrEmpty(dr["TELEFONO"].ToString()) ? null : dr["TELEFONO"].ToString(),
                            ACTIVO = Convert.ToBoolean(dr["ACTIVO"]),
                            FECHAHORACREACION = string.IsNullOrEmpty(dr["FECHAHORACREACION"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FECHAHORACREACION"]),
                            FECHAHORAULTIMOINGRESO = string.IsNullOrEmpty(dr["FECHAHORAULTIMOINGRESO"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FECHAHORAULTIMOINGRESO"]),
                            FECHAHORAINGRESO = string.IsNullOrEmpty(dr["FECHAHORAINGRESO"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FECHAHORAINGRESO"])
                        };
                    }
                }
            }
            return user;
        }

        public string ObtenerTokenConfirmacion(int codUsuarioInfo) {
            string result = string.Empty;
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_TokenConfirmacionUsuario_Solicitar";
                    command.Parameters.Add(command.CreateParameter("@CODSUARIOINFO", codUsuarioInfo));
                    SqlParameter outputIdParam = new SqlParameter("@TOKEN", SqlDbType.VarChar, 256)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);
                    command.ExecuteNonQuery();
                    if (outputIdParam.Value != DBNull.Value) {
                        result = outputIdParam.Value.ToString();
                    }
                }
                return result;
            }
        }

        public ValidacionToken ValidarTokenConfirmacion(string email, string token, bool validar)
        {
            ValidacionToken result = new ValidacionToken();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_TokenConfirmacionUsuario_Validar";
                    command.Parameters.Add(command.CreateParameter("@EMAIL", email));
                    command.Parameters.Add(command.CreateParameter("@TOKEN", token));
                    command.Parameters.Add(command.CreateParameter("@VALIDAR ", validar));
                    SqlParameter outputValido = new SqlParameter("@VALIDO", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputValido);

                    SqlParameter outputMensajeError = new SqlParameter("@MENSAJERROR", SqlDbType.VarChar, 1000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputMensajeError);
                    command.ExecuteNonQuery();
                    if (outputValido.Value != DBNull.Value)
                    {
                        result.EsValido = Convert.ToBoolean(outputValido.Value);
                    }

                    if (outputMensajeError.Value != DBNull.Value)
                    {
                        result.Mensaje = outputMensajeError.Value.ToString();
                    }
                }
                return result;
            }
        }

        public bool ActualizarDatos(int codUsuario, string email, string celular)
        {
            bool result = false;
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_ActualizarDatos";
                    command.Parameters.Add(command.CreateParameter("@CODUSUARIO", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@EMAIL", email));
                    command.Parameters.Add(command.CreateParameter("@CELULAR", celular));
                    command.ExecuteNonQuery();
                    result = true;
                }
                return result;
            }
        }

        public bool ValidarVencimientoPassword(string CODASPNETUSER, out string FechaHoraUltimoCambio)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Seguridad_ValidarVencimientoPassword";
                    command.Parameters.Add(command.CreateParameter("@AspUserID", CODASPNETUSER));

                    SqlParameter RequiereCambioPassword = new SqlParameter("@RequiereCambioPassword", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    SqlParameter FechaHoraUltimoCambioPassword = new SqlParameter("@FechaHoraUltimoCambioPassword", SqlDbType.Date) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(RequiereCambioPassword);
                    command.Parameters.Add(FechaHoraUltimoCambioPassword);

                    command.ExecuteNonQuery();

                    if (FechaHoraUltimoCambioPassword.Value != null)
                        FechaHoraUltimoCambio = FechaHoraUltimoCambioPassword.Value.ToString();
                    else
                        FechaHoraUltimoCambio = "";
                                       
                    if ((bool) RequiereCambioPassword.Value)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}

