namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using System.Collections.Generic;

    public interface ICicloFacturacionService
    {
        IEnumerable<CicloFacturacion> GetCicloFacturacion(bool incluyeUltimoCiclo); //int historicosemanas
    }
}
