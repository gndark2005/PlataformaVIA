namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class PagoRepository : AData<Pago>, IPagoRepository
    {
        public IEnumerable<Pago> GetPagosXRazonSocial(CriterioBusquedaCicloFacturacion filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RazonSocial_MisPagos"; //TODO Cambiar por nombre real

                        command.Parameters.Add(command.CreateParameter("@CodRazonSocial", filtro.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", filtro.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", filtro.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@Filtro", filtro.Filtro));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", filtro.Paginacion.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.TamanoPagina));

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
        }

        public IEnumerable<Pago> GetPagosXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RazonSocial_MisPagos";
                        command.Parameters.Add(command.CreateParameter("@CodRazonSocial", filtro.IdPadre));
                        command.Parameters.Add(command.CreateParameter("@CodUsuario", filtro.CodUsuario));
                        command.Parameters.Add(command.CreateParameter("@CodCicloFacturacion", filtro.CodCicloFacturacion));
                        command.Parameters.Add(command.CreateParameter("@CodTipoFiltro", filtro.CodTipoFiltro));
                        command.Parameters.Add(command.CreateParameter("@Filtro", filtro.Valor));
                        command.Parameters.Add(command.CreateParameter("@NumeroPagina", filtro.Paginacion.NumeroPagina + 1));
                        command.Parameters.Add(command.CreateParameter("@TamanoPagina", filtro.Paginacion.TamanoPagina));

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
        }

        public Barcode GetBarcodes(int codUsuario, bool esCadena, int codigo) {
            Barcode barcode = new Barcode();
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Barcode_GetBarcodeInfo";

                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@EsCadena", esCadena));
                    command.Parameters.Add(command.CreateParameter("@Codigo", codigo));

                    return this.ToList<Barcode>(command).First();
                }
            }
        }


        public IEnumerable<DistribucionPago> GetDistribucionPago(int id)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetDistribucionesXPago"; //TODO Cambiar por nombre real
                    command.Parameters.Add(command.CreateParameter("@CodPago", id));
                    return this.ToList<DistribucionPago>(command);
                }
            }

        }

        public InicioSaldosFacturacion GetSaldoFacturacion(int idUser)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Inicio_GetSaldosFacturacion";
                    command.Parameters.Add(command.CreateParameter("@IdUsuario", idUser));

                    return this.ToList<InicioSaldosFacturacion>(command).First();
                }
            }
        }
    }
}
