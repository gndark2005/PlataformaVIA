namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.Media;
    using PlataformaVIA.Core.Domain.AdministradorDocumentos;

    public interface ITerminalService
    {
        ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response);
        ResponseEO<Articulo> GetArticulosByInstructivo(ResponseEO<Articulo> response);
        ResponseEO<Articulo> GetArticulosByCategoriayTerminal(ResponseEO<Articulo> response);
        ResponseEO<Instructivos> GetInstructivosxProducto(ResponseEO<Instructivos> response);
        bool ChangeTerminalPassword(int codusuario, string oldTerminalPassword, string newTerminalPassword);
    }
}
