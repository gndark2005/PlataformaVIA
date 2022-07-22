namespace PlataformaVIA.Data.Repositories.Interfaces
{
    using Core.Domain.AppMobile;
    using System;
    using System.Collections.Generic;

    public interface IAppMobileRepository
    {
        IEnumerable<ProductoComercial> GetProductosByFiltro(string filtro);
        ProductoComercialDetalle GetProducto(int id, string tipo);
        Banner GetBanner();
        IEnumerable<Categoria> GetCategoriasByFechaSincronizacion(DateTime pultimaFechaSincronizacion);
        IEnumerable<InstructivoTerminal> GetInstructivosByTerminal(int tipoTerminal);
        IEnumerable<InstructivoTerminal> GetInstructivosByProductoYTerminal(int tipoTerminal, int CodProducto);
        IEnumerable<Instructivo> GetInstructivos();
        Instructivo GetInstructivoByTerminalYCategoria(int CodTipoTerminal, int CodCategoria);
        Instructivo GetInstructivoById(int codInstructivo);
        Articulo GetImagenByCodInstructivoImagen(int CodInstructivoImagen);
        Noticia GetNoticia();
        IEnumerable<Punto> GetPuntosCercanos(double longitud, double latitud);
    }
}
