namespace PlataformaVIA.Services.Implementations
{
    using System.Collections.Generic;
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Data.Repositories.Interfaces;
    using PlataformaVIA.Services.Interfaces;

    public class AdministracionCorreoService : IAdministracionCorreoService
    {
        public IAdministracionCorreoRepository AdministracionCorreoRepository { get; }

        public AdministracionCorreoService(IAdministracionCorreoRepository administracionCorreoRepository)
        {
            this.AdministracionCorreoRepository = administracionCorreoRepository;
        }

        public ResponseEO<AdministracionCorreo> GetCorreos() {
            return this.AdministracionCorreoRepository.GetCorreos();
        }
        
        public void SetEstadoCorreos(List<AdministracionCorreo> objCorreos)
        {
            this.AdministracionCorreoRepository.SetEstadoCorreos(objCorreos);
        }

        public bool SendEmail(ResponseIndividualEO<AdministracionCorreo> correoNuevo) {
            return this.AdministracionCorreoRepository.SendEmail(correoNuevo);
        }

        public int AddCorreoPendiente(AdministracionCorreo objCorreo)
        {
            return this.AdministracionCorreoRepository.AddCorreoPendiente(objCorreo);
        }
    }
}
