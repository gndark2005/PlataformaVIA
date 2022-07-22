using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class RegistroRepository : AData<Registro>, IRegistroRepository
    {

        public Registro RegistrarEvento(Registro objRegistro)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Registro_Agregar";
                        command.Parameters.Add(command.CreateParameter("@CODTIPOREGISTRO", (int)objRegistro.TipoRegistro));
                        command.Parameters.Add(command.CreateParameter("@EMAILUSUARIOINFO", objRegistro.Usuario.EMAIL));
                        command.Parameters.Add(command.CreateParameter("@NAVEGADOR", objRegistro.Navegador));
                        command.Parameters.Add(command.CreateParameter("@MENSAJE", objRegistro.Mensaje));
                        command.Parameters.Add(command.CreateParameter("@INNEREXCEPTION", objRegistro.inneException));
                        command.Parameters.Add(command.CreateParameter("@PATH", objRegistro.Path));
                        command.Parameters.Add(command.CreateParameter("@ESDISPOSITIVOMOVIL", objRegistro.DispositivoMovil));
                        command.Parameters.Add(command.CreateParameter("@PARAMETROS", objRegistro.Parametros));
                        command.Parameters.Add(command.CreateParameter("@FECHAHORAEVENTO", objRegistro.FechaEvento));
                        command.Parameters.Add(command.CreateParameter("@IP", objRegistro.Ip));

                        SqlParameter outputIdParam = new SqlParameter("@ID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                        {
                            objRegistro.IdError = Convert.ToInt32(outputIdParam.Value);
                        }
                    }
                }

                return objRegistro;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
