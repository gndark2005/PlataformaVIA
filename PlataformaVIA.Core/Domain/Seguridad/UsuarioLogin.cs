namespace PlataformaVIA.Core.Domain.Seguridad
{
    public class UsuarioLogin
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public TipoUsuarioEnum TipoLogin { get; set; }
    }
}
