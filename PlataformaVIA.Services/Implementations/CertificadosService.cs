using PlataformaVIA.Core.Domain.Certificados;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;
using System.Collections.Generic;

namespace PlataformaVIA.Services.Implementations
{
    public class CertificadosService : ICertificadosService
    {
        private ICertificadoRepository _certificadoRepository;


        public CertificadosService(ICertificadoRepository certificadoRepository)
        {
            this._certificadoRepository = certificadoRepository;
        }

        public IEnumerable<TipoDeCertificado> GetTipoCertificado(int codUsuario)
        {
            return _certificadoRepository.GetTipoCertificado(codUsuario);
        }

        public IEnumerable<FechaCertificado> GetFechaCertificado(int codCertificado, int codUsuario)
        {
            return _certificadoRepository.GetFechaCertificado(codCertificado, codUsuario);
        }

        public Certificado GenerarCertificado(int codCertificado, int codUsuario, string fecha)
        {
            return _certificadoRepository.GenerarCertificado(codCertificado, codUsuario, fecha);
        }

        public string ObtenerRutaDeStoragePorToken(string token)
        {
            return _certificadoRepository.ObtenerRutaDeStoragePorToken(token);
        }

        public bool ActualizarEstadoPorToken(string token) {
            return _certificadoRepository.ActualizarEstadoPorToken(token);
        }
    }
}