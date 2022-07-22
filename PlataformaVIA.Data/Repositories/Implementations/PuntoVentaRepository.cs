namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.Busqueda;
    using Core.Domain.Media;
    using Core.Domain.PuntoDeVenta;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using PlataformaVIA.Core.Domain.Reportes;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class PuntoVentaRepository : AData<PuntodeVenta>, IPuntoVentaRepository
    {
        private static PuntoVentaRepository instance = null;
        public static PuntoVentaRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PuntoVentaRepository();
                }

                return instance;
            }
        }

        #region "Puntos de venta"
        public ResponseEO<PuntodeVenta> ConsultarPuntoDeVenta(ResponseEO<PuntodeVenta> parametros)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_MisPuntosDeVenta";

                    command.Parameters.Add(command.CreateParameter("@CodUsuario", parametros.IdUsuario));
                    command.Parameters.Add(command.CreateParameter("@TextoBusqueda", parametros.TextoBusqueda));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", parametros.NumeroPagina + 1));
                    command.Parameters.Add(command.CreateParameter("@TamanoPagina", parametros.TamanoPagina));
                    if (!string.IsNullOrEmpty(parametros.FiltrosCriterio.Filtro))
                        command.Parameters.Add(command.CreateParameter("@TipoBusqueda", parametros.FiltrosCriterio.Filtro));

                    SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);
                    parametros.Entidades = this.ToList(command).ToList();

                    if (outputIdParam.Value != DBNull.Value)
                        parametros.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                    parametros.TotalPaginas = Convert.ToInt32(Math.Ceiling(((decimal)parametros.TotalRegistros / (decimal)parametros.TamanoPagina)));
                }
            }
            return parametros;
        }

        public ResponseIndividualEO<DetalleProducto> ConsultarDetalleProducto(ResponseIndividualEO<DetalleProducto> parametros)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetDetalleProducto";
                        command.Parameters.Add(command.CreateParameter("@EsSubproducto ", parametros.Entidad.EsSubproducto));
                        command.Parameters.Add(command.CreateParameter("@IdProducto ", parametros.IdBusqueda));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", parametros.IdUsuario));
                        parametros.Entidad = this.ToList<DetalleProducto>(command).FirstOrDefault();
                    }
                }
                return parametros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseIndividualEO<PuntodeVenta> ConsultarInformacionPuntoDeVenta(ResponseIndividualEO<PuntodeVenta> parametros)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_Inicio_Informacion";
                    command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", parametros.IdBusqueda));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", parametros.IdUsuario));
                    parametros.Entidad = this.ToList(command).FirstOrDefault();
                }
            }
            return parametros;
        }

        public ResponseIndividualEO<UsuarioPuntoDeVenta> ConsultarUsuarioPuntoDeVenta(ResponseIndividualEO<UsuarioPuntoDeVenta> parametros)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_GetUsuarioPuntoDeVenta";
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", parametros.IdUsuario));
                    parametros.Entidad = this.ToList<UsuarioPuntoDeVenta>(command).FirstOrDefault();
                }
            }
            return parametros;
        }

        #endregion

        public ResponseEO<EstadoDeCuentaGeneral> GetEstadoCuentaGeneral(ResponseEO<EstadoDeCuentaGeneral> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetEstadoCuentaGeneral";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCicloFacturacion.Paginacion.NumeroPagina));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCicloFacturacion.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        response.Entidades = this.ToList<EstadoDeCuentaGeneral>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.FiltrosCicloFacturacion.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        return response;
                    }
                }
            }
        }

        public ResponseEO<Notificaciones> GetNotificaciones(ResponseEO<Notificaciones> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Notificacion_ConsultarNotificaciones";
                    command.Parameters.Add(command.CreateParameter("@IdUsuario", response.IdUsuario));
                    response.Entidades = this.ToList<Notificaciones>(command);
                    return response;
                }
            }
        }

        public IEnumerable<PresupuestoXLineaDeNegocio> GetPresupuestosLineaNegocio(int id)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetPresupuestoXLineaNegocio"; //TODO Cambiar por nombre real
                    command.Parameters.Add(command.CreateParameter("@Id", id));
                    return this.ToList<PresupuestoXLineaDeNegocio>(command);
                }
            }
        }




        #region Nuevos Metodos

        public ResponseEO<PresupuestoXLineaDeNegocio> PresupuestoPorLineaDeNegocio(ResponseEO<PresupuestoXLineaDeNegocio> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_Inicio_PresupuestosPorLineaDeNegocio";
                    command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    response.Entidades = this.ToList<PresupuestoXLineaDeNegocio>(command);
                    return response;
                }
            }
        }

        public ResponseEO<EstadoCuentaXLineaDeNegocio> EstadosDeCuentaPorLineaDeNegocio(ResponseEO<EstadoCuentaXLineaDeNegocio> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_Inicio_EstadosCuentaPorLineaDeNegocio";
                    command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    response.Entidades = this.ToList<EstadoCuentaXLineaDeNegocio>(command);
                    return response;
                }
            }
        }

        public int AddSolicitudPrefacturacion(int Id)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_GenerarSolicitudPrefacturacion";
                    command.Parameters.Add(command.CreateParameter("@Id", Id));

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }

        public ResponseEO<Ventas> GetMisVentas(ResponseEO<Ventas> response)
        {
            ResponseEO<Ventas> obj = new ResponseEO<Ventas>();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisVentas";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosFechas.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@FechaInicio", response.FiltrosFechas.FechaInicio));
                        command.Parameters.Add(command.CreateParameter("@FechaFin", response.FiltrosFechas.FechaFin));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosFechas.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Ventas>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            obj.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        obj.Entidades = listado;
                        return obj;
                    }
                }
            }
        }

        public ResponseEO<Transaccion> GetMisTransacciones(ResponseEO<Transaccion> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisTransacciones";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosFechas.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@FechaInicio", response.FiltrosFechas.FechaInicio));
                        command.Parameters.Add(command.CreateParameter("@FechaFin", response.FiltrosFechas.FechaFin));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosFechas.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Transaccion>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        response.Entidades = listado;
                        return response;
                    }
                }
            }
        }

        public ResponseEO<Producto> GetMisProductos(ResponseEO<Producto> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisProductos";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosFechas.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosFechas.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Producto>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        response.Entidades = listado;
                        return response;
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
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisPagos";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosCicloFacturacion.IdPadre));
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
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisAjustes";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosCicloFacturacion.IdPadre));
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

        #endregion

        public IEnumerable<PuntodeVenta> GetPuntoVentaXRazonSocial(CriterioBusqueda filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetPuntoVentaXRazonSocial"; //TODO Cambiar por nombre real

                    command.Parameters.Add(command.CreateParameter("@CodRazonSocial", filtro.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@Filtro", filtro.Filtro));
                    command.Parameters.Add(command.CreateParameter("@NumeroPagina", filtro.Paginacion.NumeroPagina + 1));
                    command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.TamanoPagina));
                    //command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.));

                    SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    var listado = this.ToList(command).ToList();

                    if (outputIdParam.Value != DBNull.Value)
                        filtro.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);


                    return listado;
                }
            }
        }

        public ResponseEO<ResumenAjustesCartera> GetResumenAjustesCartera(ResponseEO<ResumenAjustesCartera> response, bool esPagoMinimo)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenAjustesCartera";
                    command.Parameters.Add(command.CreateParameter("@Id", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", esPagoMinimo));

                    response.Entidades = this.ToList<ResumenAjustesCartera>(command);
                    return response;
                }
            }
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanal(ResumenCuadreSemanal request)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenCuadreSemanal";
                    command.Parameters.Add(command.CreateParameter("@Id", request.Id));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", request.CodUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", request.EsPagoMinimo));

                    IEnumerable<ResumenCuadreSemanal> response = this.ToList<ResumenCuadreSemanal>(command);
                    return response;
                }
            }
        }

        public ResponseEO<ResumenPagosRetirosSemanaActual> GetResumenPagosRetiros(ResponseEO<ResumenPagosRetirosSemanaActual> response, bool esPagoMinimo)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenPagosRetiros";
                    command.Parameters.Add(command.CreateParameter("@Id", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", esPagoMinimo));

                    response.Entidades = this.ToList<ResumenPagosRetirosSemanaActual>(command);
                    return response;
                }
            }
        }

        public ResponseEO<ResumenPago> GetResumenPagoTotal(ResponseEO<ResumenPago> response, bool esPagoMinimo)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenPagoMinimoTotal";
                    command.Parameters.Add(command.CreateParameter("@Id", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", esPagoMinimo));

                    response.Entidades = this.ToList<ResumenPago>(command);
                    return response;
                }
            }
        }

        public ResponseEO<DistribucionDetallePago> GetDetallePago(ResponseEO<DistribucionDetallePago> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "DepositoBancario_ObtenerDetalleAplicacion";

                            command.Parameters.Add(command.CreateParameter("@CodDepositoBancario", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);

                            SqlParameter valorOtros = new SqlParameter("@ValorOtros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(valorOtros);

                            SqlParameter valorSinIDentificar = new SqlParameter("@ValorSinIDentificar", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(valorSinIDentificar);

                            var listado = this.ToList<DistribucionDetallePago>(command).ToList();

                            if (outputIdParam.Value != DBNull.Value)
                                response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                            response.DetallesPago = new BusquedaDetallePago
                            {
                                ValorOtros = Convert.ToDecimal(valorOtros.Value),
                                ValorSinIDentificar = Convert.ToDecimal(valorSinIDentificar.Value),
                            };

                            response.Entidades = listado;
                            return response;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public ResponseEO<DistribucionDetalleAjuste> GetDetalleAjuste(ResponseEO<DistribucionDetalleAjuste> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                try
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "Ajuste_ObtenerDetalleAplicacion";

                            command.Parameters.Add(command.CreateParameter("@CodAjuste", response.FiltrosCriterio.IdPadre));
                            command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina + 1));
                            command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(outputIdParam);

                            SqlParameter fechaIngreso = new SqlParameter("@FechaHoraIngreso", SqlDbType.DateTime)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(fechaIngreso);

                            SqlParameter valorAjuste = new SqlParameter("@ValorAjuste", SqlDbType.Money)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(valorAjuste);

                            SqlParameter categoria = new SqlParameter("@Categoria", SqlDbType.VarChar, int.MaxValue)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(categoria);

                            SqlParameter descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar, int.MaxValue)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(descripcion);

                            var listado = this.ToList<DistribucionDetalleAjuste>(command).ToList();

                            if (outputIdParam.Value != DBNull.Value)
                                response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                            response.DetallesAjuste = new BusquedaDetalleAjuste
                            {
                                FechaHora = fechaIngreso.Value.ToString(),
                                Valor = valorAjuste.Value.ToString(),
                                Categoria = categoria.Value.ToString(),
                                Descripcion = descripcion.Value.ToString()
                            };

                            response.Entidades = listado;
                            return response;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public ResponseEO<ResumenCuadreSemanal> GetResumenVentasAjusteFacturacion(ResponseEO<ResumenCuadreSemanal> response, bool esPagoMinimo)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenVentasAjustesFacturacion";
                    command.Parameters.Add(command.CreateParameter("@Id", response.FiltrosCriterio.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", esPagoMinimo));

                    response.Entidades = this.ToList<ResumenCuadreSemanal>(command);
                    return response;
                }
            }
        }

        public IEnumerable<TirillaCuadreSemanal> GetTirillaCuadreSemanal(CriterioBusquedaCicloFacturacion filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_TirillaCuadreSemanal"; //TODO Cambiar por nombre real

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", filtro.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", filtro.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@Filtro", filtro.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", filtro.Paginacion.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<TirillaCuadreSemanal>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            filtro.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        return listado;
                    }
                }
            }
        }

        #region "Puntos de venta Para via dummy"

        public IEnumerable<EstadoDeCuentaGeneral> GetDetalleEstadoDeCuenta(ResponseEO<EstadoDeCuentaGeneral> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetEstadoCuentaGeneral";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCicloFacturacion.Paginacion.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCicloFacturacion.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        return this.ToList<EstadoDeCuentaGeneral>(command).ToList();
                    }
                }
            }
        }

        public IEnumerable<ProductoComercial> GetProductosComerciales(CriterioBusqueda filtro)
        {
            return new List<ProductoComercial> { new ProductoComercial { Codigo = "456789", AliadoOBanco = "Occid", LineaNegocio = LineadeNegocioEnum.BillPayment, NombreProducto = "Codensa" },
                                                new ProductoComercial { Codigo = "456789", AliadoOBanco = "Occid", LineaNegocio = LineadeNegocioEnum.BillPayment, NombreProducto = "Avanti"  },
                                                new ProductoComercial { Codigo = "456789", AliadoOBanco = "Occid", LineaNegocio = LineadeNegocioEnum.BillPayment, NombreProducto = "Acueducto"  }};
        }

        public IEnumerable<ProductoComercialDetalle> GetDetalleProductoComercial(int IdProducto)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetDetalleProducto";

                        command.Parameters.Add(command.CreateParameter("@IdProducto", IdProducto));

                        var listado = this.ToList<ProductoComercialDetalle>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public IEnumerable<Transaccion> GetTransacciones(CriterioBusquedaTransaccionesPDV filtro)
        {
            return new List<Transaccion> { new Transaccion { Estado = "Exitoso", FechaHora = DateTime.Now,LineaDeNegocio = LineadeNegocioEnum.BillPayment, Producto = "DIRECT TV", Valor = 250000 },
                                               new Transaccion { Estado = "Rechazada", FechaHora = DateTime.Now,LineaDeNegocio = LineadeNegocioEnum.BillPayment, Producto = "PILA", Valor = 250000 } };
        }

        public IEnumerable<ResumenPago> GetResumenPagoTotal(long id, int ciclo)
        {
            return new List<ResumenPago> { new ResumenPago { Concepto = "Liquidación Corte", Valor = 21317395 },
                                              new ResumenPago { Concepto = "Ventas", Valor = 42081412 },
                                              new ResumenPago { Concepto = "Ajustes en Facturación", Valor = 17100 }
            };
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenCuadreSemanalPagoTotal(ResumenCuadreSemanal response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenCuadreSemanal";

                        command.Parameters.Add(command.CreateParameter("@Id", response.Id));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", response.EsPagoMinimo));

                        var listado = this.ToList<ResumenCuadreSemanal>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public IEnumerable<ResumenCuadreSemanal> GetResumenVentasAjustesSemanaActualPagoTotal(ResumenCuadreSemanal response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenCuadreSemanal";

                        command.Parameters.Add(command.CreateParameter("@Id", response.Id));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", response.EsPagoMinimo));

                        var listado = this.ToList<ResumenCuadreSemanal>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public IEnumerable<ResumenPagosRetirosSemanaActual> GetResumenPagosRetirosSemanaActualPagoTotal(CicloFacturacion request)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetRetirosSemanaActualPagoTotal";

                        command.Parameters.Add(command.CreateParameter("@Id", request.Id));
                        command.Parameters.Add(command.CreateParameter("@CodCiclo", request.CodCiclo));

                        var listado = this.ToList<ResumenPagosRetirosSemanaActual>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public IEnumerable<ResumenAjustesCartera> GetResumenAjustesCarteraPagoTotal(ResumenAjustesCartera request)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenCuadreSemanal";

                        command.Parameters.Add(command.CreateParameter("@Id", request.Id));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", request.EsPagoMinimo));

                        var listado = this.ToList<ResumenAjustesCartera>(command).ToList();

                        return listado;
                    }
                }
            }
        }

        public ResponseEO<Pago> GetPagosXCicloFacturacion(ResponseEO<Pago> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisPagos";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        var listado = this.ToList<Pago>(command).ToList();

                        if (outputIdParam.Value != DBNull.Value)
                            response.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        response.Entidades = listado;
                        return response;
                    }
                }
            }
        }

        public ResponseEO<Ajuste> GetAjustesXCicloFacturacion(ResponseEO<Ajuste> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_MisAjustes";

                        command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", response.FiltrosCicloFacturacion.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", response.FiltrosCicloFacturacion.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCicloFacturacion.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.NumeroPagina));
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

        public ResponseEO<Pago> GetPagosXCicloFacturacion(CicloFacturacion response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_GetPagosXCicloFacturacion";

                        command.Parameters.Add(command.CreateParameter("@Id", response.Id));
                        command.Parameters.Add(command.CreateParameter("@CodCiclo", response.CodCiclo));

                        ResponseEO<Pago> Result = new ResponseEO<Pago> { Entidades = this.ToList<Pago>(command).ToList() };

                        return Result;
                    }
                }
            }
        }

        public IEnumerable<ResumenPago> GetResumenPagoMinimo(ResumenPago request)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_EstadoCuentaGeneral_ResumenPagoMinimoTotal";

                        command.Parameters.Add(command.CreateParameter("@Id", request.Id));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@EsPagoMinimo", request.EsPagoMinimo));

                        var listado = this.ToList<ResumenPago>(command).ToList();

                        return listado;
                    }
                }
            }
            //return new List<ResumenPago> { new ResumenPago { Concepto = "Liquidación Corte", Valor = 21317395 },
            //                                  new ResumenPago { Concepto = "Ventas", Valor = 42081412 },
            //                                  new ResumenPago { Concepto = "Ajustes en Facturación", Valor = 17100 }
            //};
        }

        public PuntodeVenta GetDetallePuntoVenta(int id)
        {
            throw new NotImplementedException();
        }



        public bool PutRepresentanteLegal(RazonSocial response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PuntoVenta_DetallePuntoVenta_PutRepresentanteLegal";

                        command.Parameters.Add(command.CreateParameter("@Id", response.IdRazonSocial));
                        command.Parameters.Add(command.CreateParameter("@Email", response.CorreoRepresentanteLegal));
                        command.Parameters.Add(command.CreateParameter("@Celular", response.Telefono));

                        var result = command.ExecuteNonQuery();

                        return Convert.ToBoolean(result);
                    }
                }
            }
        }

        public SolicitudReporteResponse SolicitudEnvioReporte(SolicitudReporte request)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "SolicitudEnvioReporte_Agregar";

                            command.Parameters.Add(command.CreateParameter("@Codusuario", request.codUsuario));
                            command.Parameters.Add(command.CreateParameter("@CodTipoSolicitudEnvioReporte", request.codSolicitud));
                            command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", request.codPDV));
                            command.Parameters.Add(command.CreateParameter("@CodCadena", request.codCadena));
                            command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", request.codCiclo));
                            command.Parameters.Add(command.CreateParameter("@fechaReferencia", request.fechaReferencia));

                            SqlParameter isError = new SqlParameter("@HuboError", SqlDbType.Bit)
                            {
                                Direction = ParameterDirection.Output
                            };

                            SqlParameter msgError = new SqlParameter("@MensajeError", SqlDbType.VarChar, Int32.MaxValue)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(isError);
                            command.Parameters.Add(msgError);

                            command.ExecuteNonQuery();

                            SolicitudReporteResponse response = new SolicitudReporteResponse();

                            if (isError.Value != null)
                            {
                                response.HuboError = bool.Parse(isError.Value.ToString());
                                response.Mensaje = msgError.Value.ToString();
                            }
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ImagenesInstructivo ConsultarPoliticaPago(int codUsuario, int codPDV)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "PuntoVenta_DetallePuntoVenta_PoliticaPago";

                            command.Parameters.Add(command.CreateParameter("@CodPuntoVenta", codPDV));
                            command.Parameters.Add(command.CreateParameter("@Codusuario", codUsuario));

                            SqlParameter pathPolitica = new SqlParameter("@PathPoliticaImagen", SqlDbType.VarChar, Int32.MaxValue)
                            {
                                Direction = ParameterDirection.Output
                            };

                            command.Parameters.Add(pathPolitica);

                            command.ExecuteNonQuery();

                            ImagenesInstructivo response = new ImagenesInstructivo();

                            if (pathPolitica.Value != null)
                            {
                                response.Ubicacion = pathPolitica.Value.ToString();
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
        #endregion
    }
}
