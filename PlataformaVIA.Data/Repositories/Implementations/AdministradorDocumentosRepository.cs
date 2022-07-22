using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
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
    public class AdministradorDocumentosRepository : AData<Instructivos>, IAdministradorDocumentosRepository
    {
        public ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetInstructivos";

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

        public IEnumerable<TipoTerminal> GetTiposTerminal()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetTiposdeTerminal";
                    return this.ToList<TipoTerminal>(command);
                }
            }
        }

        public IEnumerable<Categorias> GetCategorias()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetCategorias";
                    return this.ToList<Categorias>(command);
                }
            }
        }

        public IEnumerable<TiposDocumento> GetTiposDocumento()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetTiposdeInstructivo";
                    return this.ToList<TiposDocumento>(command);
                }
            }
        }

        public int CrearInstructivo(Instructivos model, int idUsuario)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_CrearInstructivo";

                            command.Parameters.Add(command.CreateParameter("@TipoInstructivo", model.CodTipoInstructivo));
                            command.Parameters.Add(command.CreateParameter("@Categoria", model.CodCategoria));
                            command.Parameters.Add(command.CreateParameter("@Titulo", model.Titulo));
                            command.Parameters.Add(command.CreateParameter("@Descripcion", model.Descripcion));
                            command.Parameters.Add(command.CreateParameter("@Ubicacion", model.Ubicacion));
                            command.Parameters.Add(command.CreateParameter("@NombreArchivo", model.NombreArchivo));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.Parameters.Add(command.CreateParameter("@TipoTerminal", model.CodTipoTerminal));
                            int idInstructivo = Convert.ToInt32(command.ExecuteScalar());
                            return idInstructivo;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }           
        }

        public bool ModificarInstructivo(Instructivos model, int idUsuario)
        {
            try {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_ModificarInstructivo";

                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivo));
                            command.Parameters.Add(command.CreateParameter("@TipoInstructivo", model.CodTipoInstructivo));
                            command.Parameters.Add(command.CreateParameter("@Categoria", model.CodCategoria));
                            command.Parameters.Add(command.CreateParameter("@Titulo", model.Titulo));
                            command.Parameters.Add(command.CreateParameter("@Descripcion", model.Descripcion));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.Parameters.Add(command.CreateParameter("@TipoTerminal", model.CodTipoTerminal));
                            command.ExecuteNonQuery();
                            return true;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }            
        }

        public bool EliminarInstructivo(Instructivos model, int idUsuario)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_EliminarInstructivo";
                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivo));                           
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.ExecuteNonQuery();
                            return true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModificarInstructivoImagen(ImagenesInstructivo model, int idUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try{
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_ModificarInstructivoImagen";

                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.Parameters.Add(command.CreateParameter("@NombreArchivo", model.NombreArchivo));
                            command.Parameters.Add(command.CreateParameter("@Ubicacion", model.Ubicacion));
                            int idInstructivo = Convert.ToInt32(command.ExecuteScalar());
                            return true;
                        }
                    }
                }catch(Exception ex) {
                    return false;
                }               
            }
        }

        public bool ModificarAtributoImagen(ImagenesInstructivo model)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_ModificarAtributoImagen";
                           
                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@Atributo", model.Atributo));
                            int idInstructivo = Convert.ToInt32(command.ExecuteScalar());
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool ModificarImagenURL(ImagenesInstructivo model)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_ModificarImagenURL";

                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@URL", model.UrlImagen));
                            int idInstructivo = Convert.ToInt32(command.ExecuteScalar());
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool AgregarInstructivoImagen(ImagenesInstructivo model, int idUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_AgregarImagenI";
                          
                            command.Parameters.Add(command.CreateParameter("@CodInstructivoTerminal", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.Parameters.Add(command.CreateParameter("@Ubicacion", model.Ubicacion));
                            command.Parameters.Add(command.CreateParameter("@NombreArchivo", model.NombreArchivo));
                            command.Parameters.Add(command.CreateParameter("@Atributo", model.Atributo));
                            command.Parameters.Add(command.CreateParameter("@Url", model.UrlImagen));
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch (Exception ex) {
                    return false;
                }
            }
        }

        public bool EliminarImagenInstructivo(ImagenesInstructivo model, int idUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_EliminarInstructivoImagen";

                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch (Exception ex) { return false; }
            }
        }

        public bool CambiarOrdenImagen(ImagenesInstructivo model, int idUsuario, int accion)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Instructivo_CambiarOrdenImagen";

                            command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.IdInstructivoTipoTerminal));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            command.Parameters.Add(command.CreateParameter("@Orden", model.Orden));
                            command.Parameters.Add(command.CreateParameter("@Accion", accion));
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch (Exception ex) { return false; }
            }
        }

        public int CrearUbicacionInstructivo(InstructivoUbicacion model, int idUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Instructivo_CrearInstructivoImagen";

                        command.Parameters.Add(command.CreateParameter("@CodInstructivo", model.CodInstructivoTerminal));
                        command.Parameters.Add(command.CreateParameter("@Orden", model.Orden));
                        command.Parameters.Add(command.CreateParameter("@Ubicacion", model.Ubicacion));
                        command.Parameters.Add(command.CreateParameter("@NombreArchivo", model.NombreArchivo));
                        command.Parameters.Add(command.CreateParameter("@TipoImagen", model.TipoImagen));
                        command.Parameters.Add(command.CreateParameter("@Url", model.URL));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));                      
                        int idInstructivo = Convert.ToInt32(command.ExecuteScalar());
                        return idInstructivo;

                    }
                }
            }
        }

        public ResponseIndividualEO<Instructivos> GetDetallesInstructivo(CriterioBusqueda request)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Instructivo_GetInstructivos_DetalleInstructivo";
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodInstructivo", request.IdPadre));

                        ResponseIndividualEO<Instructivos> response = new ResponseIndividualEO<Instructivos> { Entidad = this.ToList(command).FirstOrDefault() };
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        public ResponseEO<ImagenesInstructivo> GetImagenesInstructivo(CriterioBusqueda request)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Instructivo_GetInstructivos_ImagenesInstructivo";
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodInstructivo", request.IdPadre));

                        ResponseEO<ImagenesInstructivo> response = new ResponseEO<ImagenesInstructivo> { Entidades = this.ToList<ImagenesInstructivo>(command) };
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public IEnumerable<Instructivos> GetAllInstructivoXTipo(int ID_TIPOINSTRUCTIVO)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetRutaArchivoXTipo";

                    command.Parameters.Add(command.CreateParameter("@ID_TIPOINSTRUCTIVO", ID_TIPOINSTRUCTIVO));

                    return this.ToList(command).ToList();
                }
            }
        }

        public IEnumerable<Instructivos> GetBanners(int ID_TIPOINSTRUCTIVO, bool isResponsive)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Instructivo_GetRutaArchivoXTipo";

                    command.Parameters.Add(command.CreateParameter("@ID_TIPOINSTRUCTIVO", ID_TIPOINSTRUCTIVO));

                    return this.ToList(command).ToList();
                }
            }
        }
    }
}
