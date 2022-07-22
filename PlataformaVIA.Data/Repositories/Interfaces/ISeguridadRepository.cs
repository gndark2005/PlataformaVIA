namespace PlataformaVIA.Data.Repositories.Interfaces
{
    using Core.Domain.Seguridad;
    using PlataformaVIA.Core.Domain;
    using System.Collections.Generic;

    public interface ISeguridadRepository
    {
        ResponseEO<Pregunta> GetPreguntasDeSeguridad(string Nit);
        ResponseIndividualEO<ValidacionPreguntasSeguridadResponse> ValidarPreguntasSeguridad(ValidacionPreguntasSeguridad validacionPreguntasSeguridad);
        ResponseIndividualEO<ValidacionTokenResponse> ValidarTokenDeSeguridad(string Token);
        int ObtenerRazonSocialPorToken(string token);
        UsuarioInfo GetUsuarioInfo(string Email);
        bool ValidarVencimientoPassword(string CODASPNETUSER, out string FechaHoraUltimoCambio);
        string ObtenerTokenConfirmacion(int codUsuarioInfo);
        ValidacionToken ValidarTokenConfirmacion(string email, string token, bool validar);
        bool ActualizarDatos(int codUsuario, string email, string celular);
    }
}
