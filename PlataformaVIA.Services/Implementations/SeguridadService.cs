namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain.Seguridad;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain;
    using Services.Interfaces;

    public class SeguridadService : ISeguridadService
    {
        public ISeguridadRepository SeguridadRepository { get; }

        public SeguridadService(ISeguridadRepository seguridadRepository)
        {
            this.SeguridadRepository = seguridadRepository;
        }

        public ResponseEO<Pregunta> GetPreguntasDeSeguridad(string Nit)
        {
            return this.SeguridadRepository.GetPreguntasDeSeguridad(Nit);
        }

        public ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> ValidarPreguntasSeguridad(ValidacionPreguntasSeguridad validacionPreguntasSeguridad)
        {
            return this.SeguridadRepository.ValidarPreguntasSeguridad(validacionPreguntasSeguridad);
        }

        public ResponseIndividualEO<ValidacionTokenResponse> ValidarTokenDeSeguridad(string Token)
        {
            return this.SeguridadRepository.ValidarTokenDeSeguridad(Token);
        }

        public int ObtenerRazonSocialPorToken(string token) {
            return this.SeguridadRepository.ObtenerRazonSocialPorToken(token);
        }

        public UsuarioInfo GetUsuarioInfo(string Email)
        {
            return this.SeguridadRepository.GetUsuarioInfo(Email);
        }

        public bool ValidarVencimientoPassword(string CODASPNETUSER, out string FechaHoraUltimoCambio)
        {
            return this.SeguridadRepository.ValidarVencimientoPassword(CODASPNETUSER, out FechaHoraUltimoCambio);
        }

        public string ObtenerTokenConfirmacion(int codUsuarioInfo) {
            return this.SeguridadRepository.ObtenerTokenConfirmacion(codUsuarioInfo);
        }

        public ValidacionToken ValidarTokenConfirmacion(string email, string token, bool validar)
        {
            return this.SeguridadRepository.ValidarTokenConfirmacion(email, token, validar);
        }

        public bool ActualizarDatos(int codUsuario, string email, string celular) {
            return this.SeguridadRepository.ActualizarDatos(codUsuario, email, celular);
        }
    }
}