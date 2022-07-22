namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.Cadena;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.Busqueda;
    using PlataformaVIA.Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain.Reportes;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class CadenaRepository : AData<Cadena>, ICadenaRepository
    {
        public ResponseEO<Cadena> GetCadenasXRazonSocial(ResponseEO<Cadena> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_MisCadenas"; //TODO Cambiar por nombre real
                            command.Parameters.Add(command.CreateParameter("@CodRazonSocial", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCriterio.Filtro));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCriterio.Paginacion.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCriterio.Paginacion.TamanoPagina));
                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);
                            var listado = this.ToList(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
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

        public ResponseIndividualEO<Cadena> GetInformacionCadena(CriterioBusqueda request)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Cadena_DetalleCadena_Inicio_InformacionCadena";
                        command.Parameters.Add(command.CreateParameter("@CodCadena", request.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));

                        ResponseIndividualEO<Cadena> response = new ResponseIndividualEO<Cadena>
                        {
                            Entidad = this.ToList(command).FirstOrDefault()
                        };

                        response.Entidad.EstadosCarteraxLDN = GetEstadoCartera(request.IdPadre, request.IdUsuario);
                        
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<EstadoCartera> GetEstadoCartera(int idCadena, int idUsuario)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_Inicio_EstadosCuentaPorLineaDeNegocio";
                            command.Parameters.Add(command.CreateParameter("@CodCadena", idCadena));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", idUsuario));
                            return this.ToList<EstadoCartera>(command).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseEO<EstadoPuntoVentaXLineadeNegocio> GetEstadosPuntosDeVentaAsociados(ResponseEO<EstadoPuntoVentaXLineadeNegocio> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_Inicio_EstadosPuntosDeVentaAsociados"; //TODO Cambiar por nombre real
                            command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCriterio.Filtro));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCriterio.Paginacion.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCriterio.Paginacion.TamanoPagina));
                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);
                            var listado = this.ToList<EstadoPuntoVentaXLineadeNegocio>(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value.ToString());
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

        public ResponseEO<Facturacion> GetFacturacion(ResponseEO<Facturacion> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_Facturacion";
                            command.Parameters.Add(command.CreateParameter("@CodCiclo", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                            command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCicloFacturacion.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCicloFacturacion.Paginacion.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCicloFacturacion.Paginacion.TamanoPagina));
                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);
                            var listado = this.ToList<Facturacion>(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.FiltrosCicloFacturacion.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
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

        public Cadena ConsultarDetalleCadena(Cadena response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_Id";
                            command.Parameters.Add(command.CreateParameter("@Id", response.Id));

                            using (var record = command.ExecuteReader())
                            {
                                while (record.Read())
                                {
                                    response.Id = int.Parse(record["ID_CADENA"].ToString());
                                    response.Nombre = record["NOMBRECADENA"].ToString();
                                    response.ReferenciaPago = record["REFERENCIAPAGO"].ToString();
                                    response.EstadosCarteraxLDN = GetEstadoCartera(response.Id, 1);
                                }
                            }

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

        public ResponseEO<DetalleCarteraPuntoVenta> GetDetalleCarteraPorPDV(ResponseEO<DetalleCarteraPuntoVenta> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_CarteraPorPDV";
                            command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCicloFacturacion.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCicloFacturacion.Paginacion.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCicloFacturacion.Paginacion.TamanoPagina));
                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);
                            var listado = this.ToList<DetalleCarteraPuntoVenta>(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.FiltrosCicloFacturacion.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
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

        public ResponseEO<DetalleCupoPuntodeVenta> GetDetalleCupoPorPDV(ResponseEO<DetalleCupoPuntodeVenta> response)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Cadena_DetalleCadena_CupoPorPDV";
                            command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCicloFacturacion.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                            command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCicloFacturacion.Paginacion.NumeroPagina+1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCicloFacturacion.Paginacion.TamanoPagina));
                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);
                            var listado = this.ToList<DetalleCupoPuntodeVenta>(command);

                            if (outputIdParam.Value != DBNull.Value)
                                response.FiltrosCicloFacturacion.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);
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

        public IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerSemanas()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SolicitudEnvioReporte_ObtenerSemanas";
                       
                        var listado = this.ToList<PeriodoReporte>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public IEnumerable<PeriodoReporte> SolicitudEnvioReporteObtenerMeses()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SolicitudEnvioReporte_ObtenerMeses";

                        var listado = this.ToList<PeriodoReporte>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public ResponseEO<Pago> GetMisPagos(ResponseEO<Pago> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Cadena_DetalleCadena_MisPagos";

                        command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Pago>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        response.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)response.TotalRegistros / (decimal)response.TamanoPagina)));

                        response.Entidades = listado;
                        return response;
                    }
                }
            }
        }

        public ResponseEO<Ajuste> GetMisAjustes(ResponseEO<Ajuste> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Cadena_DetalleCadena_MisAjustes";

                        command.Parameters.Add(command.CreateParameter("@CodCadena", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Ajuste>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        response.Entidades = listado;
                        return response;
                    }
                }
            }
        }

    }
}
