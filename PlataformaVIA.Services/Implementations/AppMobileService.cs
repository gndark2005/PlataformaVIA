namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain.AppMobile;
    using Data.Repositories.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class AppMobileService : IAppMobileService
    {
        public IAppMobileRepository AppMobileRepository { get; }

        public AppMobileService(IAppMobileRepository AppMobileRepository)
        {
            this.AppMobileRepository = AppMobileRepository;
        }

        public IEnumerable<ProductoComercial> GetProductosByFiltro(string filtro)
        {
            return AppMobileRepository.GetProductosByFiltro(filtro);
        }

        public Banner GetBanner()
        {
            return AppMobileRepository.GetBanner();
        }

        public IEnumerable<Categoria> GetCategoriasByFechaSincronizacion(DateTime pultimaFechaSincronizacion)
        {
            return AppMobileRepository.GetCategoriasByFechaSincronizacion(pultimaFechaSincronizacion);
        }

        public ProductoComercialDetalle GetProducto(int id, string tipo)
        {
            return AppMobileRepository.GetProducto(id, tipo);
        }

        public IEnumerable<InstructivoTerminal> GetInstructivosByTerminal(int tipoTerminal)
        {
            return AppMobileRepository.GetInstructivosByTerminal(tipoTerminal);
        }

        public Instructivo GetInstructivoByTerminalYCategoria(int CodTipoTerminal, int CodCategoria)
        {
            return AppMobileRepository.GetInstructivoByTerminalYCategoria(CodTipoTerminal, CodCategoria);
        }

        public IEnumerable<InstructivoTerminal> GetInstructivosByProductoYTerminal(int tipoTerminal, int CodProducto)
        {
            return AppMobileRepository.GetInstructivosByProductoYTerminal(tipoTerminal, CodProducto);
        }

        public IEnumerable<Instructivo> GetInstructivos()
        {
            return AppMobileRepository.GetInstructivos();
        }

        public Instructivo GetInstructivoById(int codInstructivo)
        {
            return AppMobileRepository.GetInstructivoById(codInstructivo);
        }

        public Articulo GetImagenByCodInstructivoImagen(int CodImagenInstructivo)
        {
            return AppMobileRepository.GetImagenByCodInstructivoImagen(CodImagenInstructivo);
        }

        public Noticia GetNoticia()
        {
            return AppMobileRepository.GetNoticia();
        }

        public IEnumerable<Punto> GetPuntosCercanos(double longitud, double latitud)
        {
            return AppMobileRepository.GetPuntosCercanos(longitud, latitud);
        }
    }
}
