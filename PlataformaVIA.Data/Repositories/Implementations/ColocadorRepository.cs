namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.Colocador;
    using Core.Domain.General;
    using Core.Domain.PuntoDeVenta;
    using Core.Domain.RepresentanteLegal;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.Busqueda;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class ColocadorRepository : AData<Colocador>, IColocadorRepository
    {
        public ResponseEO<Colocador> GetColocadoresXRazonSocial(ResponseEO<Colocador> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_MisColocadores";

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

                    response.Entidades = this.ToList(command);

                    if (outputIdParam.Value != DBNull.Value)
                        response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                    return response;
                }
            }
        }

        public int CreateColocador(Colocador colocador, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_CrearColocador";

                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@Nombres", colocador.Nombres));
                    command.Parameters.Add(command.CreateParameter("@Apellidos", colocador.Apellidos));
                    command.Parameters.Add(command.CreateParameter("@CodTipoIdentificacion", colocador.CodTipoIdentificacion));
                    command.Parameters.Add(command.CreateParameter("@NumeroIdentificacion", colocador.NumeroIdentificacion));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadExpedicionCedula", colocador.CodCiudadExpedicionCedula));
                    command.Parameters.Add(command.CreateParameter("@CodGenero", colocador.CodGenero));
                    command.Parameters.Add(command.CreateParameter("@FechaNacimiento", colocador.FechaNacimiento));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadNacimiento", colocador.CodCiudadNacimiento));
                    command.Parameters.Add(command.CreateParameter("@DireccionVendedor", colocador.DireccionVendedor));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadVendedor", colocador.CodCiudadVendedor));
                    command.Parameters.Add(command.CreateParameter("@Telefono", colocador.Telefono));
                    command.Parameters.Add(command.CreateParameter("@Celular", colocador.Celular));
                    command.Parameters.Add(command.CreateParameter("@CodTipoSangre", colocador.CodTipoSangre));
                    command.Parameters.Add(command.CreateParameter("@Email", colocador.CorreoElectronico));
                    command.Parameters.Add(command.CreateParameter("@CodTipoVendedor", colocador.CodTipoColocador));
                    int idColocador = Convert.ToInt32(command.ExecuteScalar());
                    return idColocador;
                }
            }
        }

        public bool EditColocador(Colocador colocador, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_EditarColocador";
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@Id", colocador.Id));
                    command.Parameters.Add(command.CreateParameter("@Nombres", colocador.Nombres));
                    command.Parameters.Add(command.CreateParameter("@Apellidos", colocador.Apellidos));
                    command.Parameters.Add(command.CreateParameter("@CodTipoIdentificacion", colocador.CodTipoIdentificacion));
                    command.Parameters.Add(command.CreateParameter("@NumeroIdentificacion", colocador.NumeroIdentificacion));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadExpedicionCedula", colocador.CodCiudadExpedicionCedula));
                    command.Parameters.Add(command.CreateParameter("@CodGenero", colocador.CodGenero));
                    command.Parameters.Add(command.CreateParameter("@FechaNacimiento", colocador.FechaNacimiento));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadNacimiento", colocador.CodCiudadNacimiento));
                    command.Parameters.Add(command.CreateParameter("@DireccionVendedor", colocador.DireccionVendedor));
                    command.Parameters.Add(command.CreateParameter("@CodCiudadVendedor", colocador.CodCiudadVendedor));
                    command.Parameters.Add(command.CreateParameter("@Telefono", colocador.Telefono));
                    command.Parameters.Add(command.CreateParameter("@Celular", colocador.Celular));
                    command.Parameters.Add(command.CreateParameter("@CodTipoSangre", colocador.CodTipoSangre));
                    command.Parameters.Add(command.CreateParameter("@Email", colocador.CorreoElectronico));
                    command.Parameters.Add(command.CreateParameter("@CodTipoVendedor", colocador.CodTipoColocador));
                    command.Parameters.Add(command.CreateParameter("@Sincronizar", colocador.Sincronizar));
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool OtorgarAccesoGlobalAColocador(int codColocador, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_OtorgarAccesoGlobal";
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@CodColocador", codColocador));
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool OtorgarAccesoPorPuntoDeVenta(List<AsignacionPuntoDeVenta> asignaciones, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    DataTable dtAsignaciones = ConvertirAsignaciones(asignaciones);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_OtorgarAccesoPorPDV";
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@DatosPDV", dtAsignaciones));
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        private DataTable ConvertirAsignaciones(List<AsignacionPuntoDeVenta> asignaciones)
        {
            DataTable dtResultado = new DataTable();
            dtResultado.Columns.Add(new DataColumn("CodPuntoDeVenta"));
            dtResultado.Columns.Add(new DataColumn("CodColocador"));
            dtResultado.Columns.Add(new DataColumn("HabilitarAcceso"));
            foreach (AsignacionPuntoDeVenta asignacion in asignaciones)
            {
                DataRow dr = dtResultado.NewRow();
                dr["CodPuntoDeVenta"] = asignacion.CodPuntoDeVenta;
                dr["CodColocador"] = asignacion.CodColocador;
                dr["HabilitarAcceso"] = asignacion.Asignado;
                dtResultado.Rows.Add(dr);
            }
            return dtResultado;
        }

        public bool DeleteColocador(CriterioBusqueda request)
        {
            try
            {

                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_EliminarColocador";
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@Id", request.IdPadre));
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangeAccesoColocador(int codColocador, int codPuntoDeVenta, bool estado, int codUsuario)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_AsignarPuntoDeVenta";
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@CodColocador", codColocador));
                    command.Parameters.Add(command.CreateParameter("@CodPuntoDeVenta", codPuntoDeVenta));
                    command.Parameters.Add(command.CreateParameter("@EstadoNuevo", estado));
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public IEnumerable<TipoIdentificacion> GetTiposIdentificacion()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TipoIdentificacion_GetTipoIdentificacion";
                    return this.ToList<TipoIdentificacion>(command);
                }
            }
        }

        public IEnumerable<Genero> GetGeneros()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Genero_GetGeneros";
                    return this.ToList<Genero>(command);
                }
            }
        }

        public IEnumerable<TipoSangre> GetTiposSangre()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TipoSangre_GetTiposSangre";
                    return this.ToList<TipoSangre>(command);
                }
            }
        }

        public IEnumerable<TipoVendedor> GetTiposVendedor()
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TipoColocador_GetTiposColocador";
                    return this.ToList<TipoVendedor>(command);
                }
            }
        }

        public IEnumerable<Ciudad> GetCiudadesByFilter(string filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Ciudades_GetCiudadesByFilter";
                    command.Parameters.Add(command.CreateParameter("@Filter", filtro));
                    return this.ToList<Ciudad>(command);
                }
            }
        }

        public ResponseIndividualEO<Colocador> GetDetalleColocador(CriterioBusqueda request)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Colocadores_GetDetalleColocador";
                    command.Parameters.Add(command.CreateParameter("@Sincronizar", request.Filtro == "1" ? true : false));
                    command.Parameters.Add(command.CreateParameter("@Id", request.IdPadre));
                    command.Parameters.Add(command.CreateParameter("@CodUsuario", request.IdUsuario));

                    ResponseIndividualEO<Colocador> response = new ResponseIndividualEO<Colocador> { Entidad = this.ToList(command).FirstOrDefault() };
                    return response;
                }
            }
        }

        public IEnumerable<PuntosVentaAcceso> GetPuntosdeventaXRazonSocialYColocador(int idcolocador)
        {
            throw new NotImplementedException();
            //using (var context = new DbContext(new DbConnectionFactory()))
            //{
            //    using (var command = context.CreateCommand())
            //    {
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            command.CommandText = "Empresa_Colocadores_GetPuntosdeventaXRazonSocialYColocador"; //TODO Cambiar por nombre real

            //            command.Parameters.Add(command.CreateParameter("@CodColocador", filtro.IdPadre));
            //            command.Parameters.Add(command.CreateParameter("@Filtro", filtro.Filtro));
            //            command.Parameters.Add(command.CreateParameter("@NumeroPagina", filtro.Paginacion.NumeroPagina));
            //            command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.TamanoPagina));

            //            SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
            //            {
            //                Direction = ParameterDirection.Output
            //            };

            //            command.Parameters.Add(outputIdParam);

            //            var listado = this.ToList<PuntodeVenta>(command);

            //            if (outputIdParam.Value != DBNull.Value)
            //                filtro.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

            //            return listado;
            //        }
            //    }
            //}
        }

        public ResponseEO<PuntodeVenta> GetPuntosVentaAccesoColocador(ResponseEO<PuntodeVenta> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_GetPuntosDeVentaXColocador";

                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodColocador", response.FiltrosCriterio.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCriterio.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCriterio.Paginacion.NumeroPagina));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCriterio.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        response.Entidades = this.ToList<PuntodeVenta>(command);

                        if (outputIdParam.Value != DBNull.Value)
                            response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        return response;
                    }
                }
            }
        }

        public ResponseEO<PuntodeVenta> GetPuntosVentaPorRepresentante(ResponseEO<PuntodeVenta> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_GetPuntosDeVentaXRepresentante";

                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        command.Parameters.Add(command.CreateParameter("@Filtro", response.FiltrosCriterio.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", response.FiltrosCriterio.Paginacion.NumeroPagina));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", response.FiltrosCriterio.Paginacion.TamanoPagina));

                        SqlParameter outputIdParam = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputIdParam);

                        response.Entidades = this.ToList<PuntodeVenta>(command);

                        if (outputIdParam.Value != DBNull.Value)
                            response.FiltrosCriterio.Paginacion.TotalRegistros = Convert.ToInt32(outputIdParam.Value);

                        return response;
                    }
                }
            }
        }

        //private DbContext _context;

        //#region Constructores
        //public ColocadorRepository(DbContext context) : base(context)
        //{
        //    this._context = context;
        //}
        //#endregion

        public bool OtorgarAcceso(AccesoColocador accesocolocador)
        {
            try
            {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Empresa_Colocadores_OtorgarAccesoColocador";

                        var accesocolocadorparam = new SqlParameter("@AccesoColocadorTableType", SqlDbType.Structured)
                        {
                            TypeName = "dbo.AccesoColocadorTableType",
                            Value = accesocolocador
                        };
                        command.Parameters.Add(accesocolocadorparam);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ResponseEO<Colocador> ConsultarColocadoresParaSincronizarSAG(ResponseEO<Colocador> response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_GetColocadoresParaSincronizarSAG";
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", response.IdUsuario));
                        response.Entidades = this.ToList(command);
                        return response;
                    }
                }
            }
        }

        public ResponseEO<Colocador> Colocadores_GetDetalleColocadorXNumeroIdentificacion(Colocador response)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_GetDetalleColocadorXNumeroIdentificacion";
                        command.Parameters.Add(command.CreateParameter("@NUMEROIDENTIFICACION", response.NumeroIdentificacion));

                        ResponseEO<Colocador> respuesta = new ResponseEO<Colocador>()
                        {
                            Entidades = this.ToList(command)
                        };

                        return respuesta;
                    }
                }
            }
        }

        public string ConfirmarSincronizacionConSAG(List<int> ltIdColocadores)
        {

            DataTable dtInt = new DataTable();
            dtInt.Columns.Add("IdValie");

            foreach (var item in ltIdColocadores)
            {
                DataRow dr = dtInt.NewRow();
                dr["IdValie"] = item;

                dtInt.Rows.Add(dr);
            }

            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Colocadores_ColocadorParaSincronizar_ConfirmarSincronizacion";
                        command.Parameters.Add(command.CreateParameter("@IDSCOLOCADORARASINCRONIZAR", dtInt));

                        SqlParameter Respuesta = new SqlParameter("@MENSAJEERROR", SqlDbType.VarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(Respuesta);
                        command.ExecuteNonQuery();

                        return Respuesta.ToString();
                    }
                }
            }
        }
    }
}
