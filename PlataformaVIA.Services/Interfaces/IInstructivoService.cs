namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.Media;
    using System.Collections.Generic;

    public partial interface IInstructivoService
    {
        IEnumerable<Instructivo> GetAllInstructivoXTipoTerminal(TipoTerminalEnum tipoTerminal);
        Instructivo GetInstructivo(int codCategoria, TipoTerminalEnum tipoTerminal);
        IEnumerable<Articulo> GetArticuloXInstructivo(int codInstructivo);
    }
}
