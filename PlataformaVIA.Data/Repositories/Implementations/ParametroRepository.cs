using PlataformaVIA.Core.Domain.Parametro;
using PlataformaVIA.Data.Extensions;
using PlataformaVIA.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Data.Repositories.Implementations
{
    public class ParametroRepository: AData<Parametro>, IParametroRepository
    {
        public Parametro BuscarParametro(int codUsuario, string keyParametro)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {
                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Parametro_GetParametro";

                    command.Parameters.Add(command.CreateParameter("@CodUsuario", codUsuario));
                    command.Parameters.Add(command.CreateParameter("@KeyParametro", keyParametro));
                    
                    return this.ToList<Parametro>(command).First();
                }
            }
        }
    }
}
