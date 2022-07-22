using PlataformaVIA.Core.Domain.Certificados;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class CertificadoRepository : AData<TipoDeCertificado>, ICertificadoRepository
    {
        public IEnumerable<TipoDeCertificado> GetTipoCertificado(int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Certificados_GetTipoCertificados";

                    command.Parameters.Add(command.CreateParameter("@CodUusario", codUsuario));

                    var listado = this.ToList(command).ToList();

                    return listado;
                }
            }
        }

        public IEnumerable<FechaCertificado> GetFechaCertificado(int codCertificado, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Certificados_GetMes_CodCertificado";

                    command.Parameters.Add(command.CreateParameter("@CodCertificado", codCertificado));
                    command.Parameters.Add(command.CreateParameter("@CodUusario", codUsuario));

                    return this.ToList<FechaCertificado>(command);
                }
            }
        }

        public Certificado GenerarCertificado(int codCertificado, int codUsuario, string fecha)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Certificados_GenerarCertificado";

                    command.Parameters.Add(command.CreateParameter("@CodUusario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@CodCertificado", codCertificado));
                    command.Parameters.Add(command.CreateParameter("@Fecha", fecha));

                    return this.ToList<Certificado>(command).First();
                }
            }
        }

        public string ObtenerRutaDeStoragePorToken(string token)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AdministracionCorreoNube_ObtenerRutaStoragePorToken";

                        command.Parameters.Add(command.CreateParameter("@TOKEN", token));
                        string resultado =Convert.ToString(command.ExecuteScalar());
                        if (string.IsNullOrEmpty(resultado)) {
                            throw new Exception("Token no valido");
                        }
                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool ActualizarEstadoPorToken(string token)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "AdministracionCorreoNube_actualizarestadocorreoportoken";

                        command.Parameters.Add(command.CreateParameter("@TOKEN", token));
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
