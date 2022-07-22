namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using System.Collections.Generic;

    public interface IAjusteService
    {
        IEnumerable<Ajuste> GetAjustesXRazonSocial(CriterioBusquedaCicloFacturacion filtro);
        IEnumerable<Ajuste> GetAjustesXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro);
        IEnumerable<DistribucionAjuste> GetDistribucionAjuste(int id);
    }
}
