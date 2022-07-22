namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Data.Repositories.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class CicloFacturacionService : ICicloFacturacionService
    {
        private ICicloFacturacionRepository _ciclofacturacionRepository;


        public CicloFacturacionService(ICicloFacturacionRepository ciclofacturacionRepository)
        {
            this._ciclofacturacionRepository = ciclofacturacionRepository;
        }

        public IEnumerable<CicloFacturacion> GetCicloFacturacion(bool incluyeUltimoCiclo)//int historicosemanas
        {
            return _ciclofacturacionRepository.GetUltimosCicloFacturacion(incluyeUltimoCiclo);
        }

        
    }
}
