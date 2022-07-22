using PlataformaVIA.Core.Domain.Certificados;
using System.Collections.Generic;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface ICertificadoRepository
    {
        IEnumerable<TipoDeCertificado> GetTipoCertificado(int codUsuario);
        IEnumerable<FechaCertificado> GetFechaCertificado(int codCertificado, int codUsuario);
        Certificado GenerarCertificado(int codCertificado, int codUsuario, string fecha);
        string ObtenerRutaDeStoragePorToken(string token);
        bool ActualizarEstadoPorToken(string token);
    }
}
