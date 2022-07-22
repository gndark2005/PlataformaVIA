namespace PlataformaVIA.Core.Domain.Seguridad
{
    using System;
    using System.Collections.Generic;

    public class UsuarioSistema
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public string CodigoPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        public short DigitoVerificacion { get; set; }
        public DateTime FechaHoraUltimoIngreso { get; set; }
        public IEnumerable<Pregunta> PreguntasSeguridad { get; set; }
    }
}

