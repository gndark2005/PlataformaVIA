namespace PlataformaVIA.Data.Repositories.Interfaces
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using System.Collections.Generic;

    public interface IAjusteRepository
    {
        IEnumerable<Ajuste> GetAjustesXRazonSocial(CriterioBusquedaCicloFacturacion filtro);
        IEnumerable<Ajuste> GetAjustesXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro);
        IEnumerable<DistribucionAjuste> GetDistribucionAjuste(int id);
    }
}
