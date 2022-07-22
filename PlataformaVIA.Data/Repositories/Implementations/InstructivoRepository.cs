namespace PlataformaVIA.Data.Repositories.Implementations
{
    using Core.Domain;
    using Core.Domain.Media;
    using Data.Extensions;
    using Data.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class InstructivoRepository : AData<Instructivo>, IInstructivoRepository
    {
        public IEnumerable<Instructivo> GetAllInstructivoXTipoTerminal(TipoTerminalEnum tipoTerminal)
        {
            using (var context = new DbContext(new DbConnectionFactory()))
            {

                using (var command = context.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetAllInstructivoXTipoTerminal";

                    command.Parameters.Add(command.CreateParameter("@CodTipoTerminal", (int)tipoTerminal));

                    return this.ToList(command).ToList();
                }
            }
        }

        public IEnumerable<Articulo> GetArticuloXInstructivo(int codInstructivo)
        {
            throw new NotImplementedException();
        }

        public Instructivo GetInstructivo(int codCategoria, TipoTerminalEnum tipoTerminal)
        {
            throw new NotImplementedException();
        }
    }
}
