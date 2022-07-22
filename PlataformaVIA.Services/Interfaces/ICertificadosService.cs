using PlataformaVIA.Core.Domain.Certificados;
using System.Collections.Generic;

namespace PlataformaVIA.Services.Interfaces
{
    public interface ICertificadosService
    {       
        IEnumerable<TipoDeCertificado> GetTipoCertificado(int codUsuario);
        IEnumerable<FechaCertificado> GetFechaCertificado(int codCertificado, int codUsuario);
        Certificado GenerarCertificado(int codCertificado, int codUsuario, string fecha);
        string ObtenerRutaDeStoragePorToken(string token);
        bool ActualizarEstadoPorToken(string token);
    }
}
