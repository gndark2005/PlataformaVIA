namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class AjusteRepository : AData<Ajuste>, IAjusteRepository
    {
        public IEnumerable<Ajuste> GetAjustesXRazonSocial(CriterioBusquedaCicloFacturacion filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RazonSocial_MisAjustes";

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

        public IEnumerable<Ajuste> GetAjustesXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RazonSocial_MisAjustes";

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

        public IEnumerable<DistribucionAjuste> GetDistribucionAjuste(int id)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetDistribucionesXAjuste"; //TODO Cambiar por nombre real
                    command.Parameters.Add(command.CreateParameter("@CodPago", id));
                    return this.ToList<DistribucionAjuste>(command);
                }
            }
        }
    }
}
