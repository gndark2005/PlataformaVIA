namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class PagoService : IPagoService
    {
        public IPagoRepository PagoRepository { get; }

        public PagoService(IPagoRepository pagoRepository)
        {
            this.PagoRepository = pagoRepository;
        }

        public IEnumerable<Pago> GetPagosXRazonSocial(CriterioBusquedaCicloFacturacion filtro)
        {
            return this.PagoRepository.GetPagosXRazonSocial(filtro);
        }

        public IEnumerable<Pago> GetPagosXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro)
        {
            return this.PagoRepository.GetPagosXRazonSocialFiltro(filtro);
        }

        public IEnumerable<DistribucionPago> GetDistribucionPago(int id)
        {
            return this.PagoRepository.GetDistribucionPago(id);
        }

        public InicioSaldosFacturacion GetSaldoFacturacion(int idUser)
        {
            return this.PagoRepository.GetSaldoFacturacion(idUser);
        }

        public Barcode GetBarcodes(int codUsuario, bool esCadena, int codigo) {
            return this.PagoRepository.GetBarcodes(codUsuario, esCadena, codigo);
        }
    }
}