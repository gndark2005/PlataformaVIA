namespace PlataformaVIA.Core.Domain.Seguridad
{
    public class ValidacionTokenResponse
    {
        public string Token { get; set; }
        public bool Valido { get; set; }
        public int CodRazonSocial { get; set; }
        public string MensajeError { get; set; }
    }
}
