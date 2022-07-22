using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Busqueda;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class DivulgacionRepository : AData<Divulgacion>, IDivulgacionRepository
    {
        private static DivulgacionRepository instance = null;
        public static DivulgacionRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DivulgacionRepository();
                }

                return instance;
            }
        }

        public ResponseEO<Divulgacion> GetDivulgaciones(ResponseEO<Divulgacion> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Divulgacion_ObtenerDivulgaciones";

                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCriterio.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCriterio.Paginacion.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCriterio.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        response.Entidades = this.ToList(command);

                        if (outputIdParam.Value != DBNull.Value)
                            response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int AgregarDivulgacion(Divulgacion model)
        {

            int idReturn = 0;

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_CrearDivulgacion";
                   
                    command.Parameters.Add(command.CreateParameter("@NOMBRE", model.NOMBRE));
                    command.Parameters.Add(command.CreateParameter("@TITULO", model.TITULO));
                    command.Parameters.Add(command.CreateParameter("@TEXTO", model.TEXTO));
                    command.Parameters.Add(command.CreateParameter("@FECHAINICIO", model.FECHAINICIO));
                    command.Parameters.Add(command.CreateParameter("@FECHAFIN", model.FECHAFIN));
                    command.Parameters.Add(command.CreateParameter("@OPCIONRECHAZAR", model.OPCIONRECHAZAR));
                    command.Parameters.Add(command.CreateParameter("@HABILITADA", model.HABILITADA));
                    command.Parameters.Add(command.CreateParameter("@CODUSUARIOINFOMODIFICACION", model.CODUSUARIOINFOMODIFICACION));
                    command.Parameters.Add(command.CreateParameter("@POLITICATRATAMIENTO", model.POLITICADETRATAMIENTO));


                    SqlParameter outputIdParam = new SqlParameter("@IdCreado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    if (outputIdParam.Value != DBNull.Value)
                    {
                        idReturn = Convert.ToInt32(outputIdParam.Value);
                    }
                        
                    return idReturn;
                }
            }
        }

        public int AgregarDivulgacionxRol(DivulgacionxRol model)
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_CrearDivulgacionXRol";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", model.CodDivulgacion));
                    command.Parameters.Add(command.CreateParameter("@CODROL", model.CodDRol));
                                        
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int AgregarExcepcionxNIT(ExcepcionxNIT model)
        {

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_CrearExcepcionxNIT";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", model.CodDivulgacion));
                    command.Parameters.Add(command.CreateParameter("@NIT", model.NIT));

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ActualizarDivulgacion(Divulgacion model)
        {
            try{
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Divulgacion_ActualizarDivulgacion";

                        command.Parameters.Add(command.CreateParameter("@ID_DIVULGACION", model.ID_DIVULGACION));
                        command.Parameters.Add(command.CreateParameter("@NOMBRE", model.NOMBRE));
                        command.Parameters.Add(command.CreateParameter("@TITULO", model.TITULO));
                        command.Parameters.Add(command.CreateParameter("@TEXTO", model.TEXTO));
                        command.Parameters.Add(command.CreateParameter("@FECHAINICIO", model.FECHAINICIO));
                        command.Parameters.Add(command.CreateParameter("@FECHAFIN", model.FECHAFIN));
                        command.Parameters.Add(command.CreateParameter("@OPCIONRECHAZAR", model.OPCIONRECHAZAR));
                        command.Parameters.Add(command.CreateParameter("@HABILITADA", model.HABILITADA));
                        command.Parameters.Add(command.CreateParameter("@CODUSUARIOINFOMODIFICACION", model.CODUSUARIOINFOMODIFICACION));
                        command.Parameters.Add(command.CreateParameter("@POLITICATRATAMIENTO", model.POLITICADETRATAMIENTO));

                        return command.ExecuteNonQuery();
                    }
                }
            } catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public ResponseIndividualEO<Divulgacion> GetDetallesDivulgacion(CriterioBusqueda request)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Divulgacion_ObtenerDivulgacionesByID";
                        //command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@ID_DIVULGACION", request.IdPadre));

                        ResponseIndividualEO<Divulgacion> response = new ResponseIndividualEO<Divulgacion> { Entidad = this.ToList(command).FirstOrDefault() };
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Rol> GetRoles()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerRoles";
                    return this.ToList<Rol>(command);
                }
            }
        }

        public IEnumerable<DivulgacionxRol> ObtenerRolesxDivulgacion(int idDiv)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerRolesxDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", idDiv));

                    return this.ToList<DivulgacionxRol>(command);
                }
            }
        }

        public IEnumerable<ExcepcionxNIT> ObtenerExcepcionesXDivulgacion(int idDiv)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerExcepcionesxDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", idDiv));

                    return this.ToList<ExcepcionxNIT>(command);
                }
            }
        }

        public IEnumerable<Divulgacion> ObtenerDivulgacionxUsuario(int idUser)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerDivulgacionxUsuario";

                    command.Parameters.Add(command.CreateParameter("@CODUSUARIO", idUser));
                    

                    return this.ToList<Divulgacion>(command);
                }
            }
        }

        public IEnumerable<ExcepcionxNIT> ValidarNitxDivulgacion(ExcepcionxNIT model)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ValidarNitxDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", model.CodDivulgacion));
                    command.Parameters.Add(command.CreateParameter("@NIT", model.NIT));


                    return this.ToList<ExcepcionxNIT>(command);
                }
            }
        }

        public IEnumerable<NitDivulgacion> ObtenerNITS(string search)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerNITS";

                    command.Parameters.Add(command.CreateParameter("@Busqueda", search));
                  
                    return this.ToList<NitDivulgacion>(command);
                }
            }
        }

        public int EliminarRolesxDivulgacion(int idDiv)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_EliminarRolesxDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", idDiv));

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int EliminarExcepcionesxDivulgacion(int idDiv)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_EliminarExcepcionesxDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", idDiv));

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int InsertarRespuestaDivulgacion(Divulgacion model)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_InsertarRespuestaDivulgacion";
                    
                command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", model.ID_DIVULGACION));
                command.Parameters.Add(command.CreateParameter("@CODUSUARIO", model.CODUSUARIOINFOMODIFICACION));
                command.Parameters.Add(command.CreateParameter("@FECHAHORA", model.FECHAHORAMODIFICACION));
                command.Parameters.Add(command.CreateParameter("@ACEPTADO", model.ACEPTADO));
                command.Parameters.Add(command.CreateParameter("@DIRECCIONIP", model.DIRECCIONIP));
                command.Parameters.Add(command.CreateParameter("@BROWSERID", model.BROWSERID));
                command.Parameters.Add(command.CreateParameter("@COMPUTERID", model.COMPUTERID));
                command.Parameters.Add(command.CreateParameter("@UBICACION", model.UBICACION));

                return command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<ResultadoDivulgacion> ObtenerResultadosDivulgacion(int idDivulgacion, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Divulgacion_ObtenerResultadosDivulgacion";

                    command.Parameters.Add(command.CreateParameter("@CODDIVULGACION", idDivulgacion));
                    command.Parameters.Add(command.CreateParameter("@FECHAINICIO", fechaInicio));
                    command.Parameters.Add(command.CreateParameter("@FECHAFIN", fechaFin.AddDays(1)));


                    return this.ToList<ResultadoDivulgacion>(command);
                }
            }
        }

    }
}
