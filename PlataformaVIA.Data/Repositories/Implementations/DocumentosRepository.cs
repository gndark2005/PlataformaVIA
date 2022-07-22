using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Documentos;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;


namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class DocumentosRepository : AData<DocumentoPDF>, IDocumentosRepository
    {      
        public ResponseEO<DocumentoPDF> GetDocumentosPDF(ResponseEO<DocumentoPDF> parametros)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Documentos_ConsultarDocumentos";

                        command.Parameters.Add(command.CreateParameter("@CodUsuario", parametros.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@TextoBusqueda", parametros.TextoBusqueda));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", parametros.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", parametros.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);
                        parametros.Entidades = this.ToList(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            parametros.TotalRegistros = Convert.ToInt32(outputIdParam.Value);                       
                    }
                }
                return parametros;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }
}
