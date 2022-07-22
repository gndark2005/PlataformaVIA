namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.Media;
    using Data.Repositories.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class InstructivoService : IInstructivoService
    {
        private IInstructivoRepository _instructivoRepository;


        public InstructivoService(IInstructivoRepository instructivoRepository)
        {
            this._instructivoRepository = instructivoRepository;
        }

        public IEnumerable<Instructivo> GetAllInstructivoXTipoTerminal(TipoTerminalEnum tipoTerminal)
        {
            return _instructivoRepository.GetAllInstructivoXTipoTerminal(tipoTerminal);
            //throw new NotImplementedException();
            //return new List<Instructivo> { new Instructivo { CodTipoTerminal = 1 } };
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
