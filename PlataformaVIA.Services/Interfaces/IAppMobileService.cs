namespace PlataformaVIA.Services.Interfaces
{
    using PlataformaVIA.Core.Domain.AppMobile;
    using System;
    using System.Collections.Generic;

    public interface IAppMobileService
    {
        IEnumerable<ProductoComercial> GetProductosByFiltro(string filtro);
        ProductoComercialDetalle GetProducto(int id, string tipo);
        Banner GetBanner();
        IEnumerable<Categoria> GetCategoriasByFechaSincronizacion(DateTime pultimaFechaSincronizacion);
        IEnumerable<InstructivoTerminal> GetInstructivosByTerminal(int tipoTerminal);
        Instructivo GetInstructivoByTerminalYCategoria(int CodTipoTerminal, int CodCategoria);
        IEnumerable<InstructivoTerminal> GetInstructivosByProductoYTerminal(int tipoTerminal, int CodProducto);
        IEnumerable<Instructivo> GetInstructivos();
        Instructivo GetInstructivoById(int codInstructivo);
        Articulo GetImagenByCodInstructivoImagen(int CodImagenInstructivo);
        Noticia GetNoticia();
        IEnumerable<Punto> GetPuntosCercanos(double longitud, double latitud);
    }
}
