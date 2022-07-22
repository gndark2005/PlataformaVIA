namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.Media;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class TerminalRepository : AData<Instructivos>, ITerminalRepository
    {
        public bool ChangeTerminalPassword(int codusuario, string oldTerminalPassword, string newTerminalPassword) {
            bool resultado = false;
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Terminal_ChangePasswordColocador";
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", codusuario));
                            command.Parameters.Add(command.CreateParameter("@OldPassword",oldTerminalPassword));
                            command.Parameters.Add(command.CreateParameter("@NewPassword", newTerminalPassword));
                            command.ExecuteNonQuery();
                            resultado = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


        public ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_GetInstructivosPorTipoTerminal"; 
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@CodTipoTerminal", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@Filtro", response.TextoBusqueda));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina+1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);

                            var listado = this.ToList(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                            response.Entidades = listado;

                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseEO<Instructivos> GetInstructivosxProducto(ResponseEO<Instructivos> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Terminal_GetInstructivosxProducto";
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@CodTipoTerminal", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodProducto", response.FiltrosCriterio.IdUsuario));

                            var listado = this.ToList<Instructivos>(command);

                            response.Entidades = listado;

                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseEO<Articulo> GetArticulosByInstructivo(ResponseEO<Articulo> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Terminal_GetArticulosByInstructivo"; 
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", response.FiltrosCriterio.IdPadre));

                            var listado = this.ToList<Articulo>(command);

                            response.Entidades = listado;

                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseEO<Articulo> GetArticulosByCategoriayTerminal(ResponseEO<Articulo> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Terminal_GetArticulosByCategoriayTerminal";
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@CodCategoria", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodTerminal", response.FiltrosCriterio.IdUsuario));

                            var listado = this.ToList<Articulo>(command);

                            response.Entidades = listado;

                            return response;
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
