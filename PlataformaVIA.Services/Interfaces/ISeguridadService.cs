namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain.Seguridad;
    using PlataformaVIA.Core.Domain;
    using System.Collections.Generic;

    public interface ISeguridadService
    {
        UsuarioInfo GetUsuarioInfo(string Email);
        ResponseEO<Pregunta> GetPreguntasDeSeguridad(string Nit);
        ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> ValidarPreguntasSeguridad(ValidacionPreguntasSeguridad validacionPreguntasSeguridad);
        ResponseIndividualEO<ValidacionTokenResponse> ValidarTokenDeSeguridad(string Token);
        int ObtenerRazonSocialPorToken(string token);
        bool ValidarVencimientoPassword(string CODASPNETUSER, out string FechaHoraUltimoCambio);
        string ObtenerTokenConfirmacion(int codUsuarioInfo);
        ValidacionToken ValidarTokenConfirmacion(string email, string token, bool validar);
        bool ActualizarDatos(int codUsuario, string email, string celular);
    }
}
