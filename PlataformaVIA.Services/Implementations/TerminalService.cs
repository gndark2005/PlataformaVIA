namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.Media;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;
    using Services.Interfaces;

    public class TerminalService : ITerminalService
    {
        public ITerminalRepository _TerminalRepository { get; }

        public TerminalService(ITerminalRepository terminalRepository)
        {
            _TerminalRepository = terminalRepository;
        }

        public ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response)
        {
            return _TerminalRepository.GetInstructivos(response);
        }

        public ResponseEO<Articulo> GetArticulosByInstructivo(ResponseEO<Articulo> response) {
            return _TerminalRepository.GetArticulosByInstructivo(response);
        }
       
        public ResponseEO<Articulo> GetArticulosByCategoriayTerminal(ResponseEO<Articulo> response)
        {
            return _TerminalRepository.GetArticulosByInstructivo(response);
        }

        public ResponseEO<Instructivos> GetInstructivosxProducto(ResponseEO<Instructivos> response)
        {
            return _TerminalRepository.GetInstructivosxProducto(response);
        }

        public bool ChangeTerminalPassword(int codusuario, string oldTerminalPassword, string newTerminalPassword) {
            return _TerminalRepository.ChangeTerminalPassword(codusuario, oldTerminalPassword, newTerminalPassword);
        }
    }
}
