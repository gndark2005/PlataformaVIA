namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class CicloFacturacionRepository : AData<CicloFacturacion>, ICicloFacturacionRepository
    {
        public IEnumerable<CicloFacturacion> GetUltimosCicloFacturacion(bool incluyeUltimoCiclo)//int historicosemanas
        {
            try {
                using (var context = new DbContext(new DbConnectionFactory()))
                {
                    using (var command = context.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "CicloFacturacion_ObtenerUltimosCiclosFacturacion"; //TODO Cambiar por nombre real Quitar parametro de historico quemado a 20 y dejarlo de una tabla de parametros en la base de datos

                        command.Parameters.Add(command.CreateParameter("@CantidadCiclos", 20));
                        command.Parameters.Add(command.CreateParameter("@IncluirCicloActal ", incluyeUltimoCiclo));

                        var listado = this.ToList(command).ToList();

                        return listado;
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