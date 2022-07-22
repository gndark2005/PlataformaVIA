namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain.Busqueda;
    using Core.Domain.PuntoDeVenta;
    using Data.Repositories.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class AjusteService : IAjusteService
    {
        public IAjusteRepository AjusteRepository { get; }

        public AjusteService(IAjusteRepository ajusteRepository)
        {
            this.AjusteRepository = ajusteRepository;
        }

        public IEnumerable<Ajuste> GetAjustesXRazonSocial(CriterioBusquedaCicloFacturacion filtro)
        {
            return this.AjusteRepository.GetAjustesXRazonSocial(filtro);
        }

        public IEnumerable<Ajuste> GetAjustesXRazonSocialFiltro(CriterioBusquedaPagoAjuste filtro)
        {
            return this.AjusteRepository.GetAjustesXRazonSocialFiltro(filtro);
        }

        public IEnumerable<DistribucionAjuste> GetDistribucionAjuste(int id)
        {
            return this.AjusteRepository.GetDistribucionAjuste(id);
        }
    }
}