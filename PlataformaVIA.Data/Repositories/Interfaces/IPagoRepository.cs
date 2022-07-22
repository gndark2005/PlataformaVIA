namespace PlataformaVIA.Data.Repositories.Interfaces
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using PlataformaVIA.Core.Domain;
    using System.Collections.Generic;

    public interface IPagoRepository
    {
        IEnumerable<Pago> GetPagosXRazonSocial(CriterioBusquedaCicloFacturacion filtro);
        IEnumerable<Pago> GetPagosXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro);
        IEnumerable<DistribucionPago> GetDistribucionPago(int id);
        InicioSaldosFacturacion GetSaldoFacturacion(int idUser);
        Barcode GetBarcodes(int codUsuario, bool esCadena, int codigo);
    }
}
