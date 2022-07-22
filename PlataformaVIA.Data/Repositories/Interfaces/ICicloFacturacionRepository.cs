namespace PlataformaVIA.Data.Repositories.Interfaces
{
    using Core.Domain;
    using System.Collections.Generic;

    public interface ICicloFacturacionRepository
    {
        IEnumerable<CicloFacturacion> GetUltimosCicloFacturacion(bool incluyeUltimoCiclo);//
    }
}
