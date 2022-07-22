namespace PlataformaVIA.Core.Domain.RepresentanteLegal
{
    using System.Collections.Generic;

    public class AccesoColocador
    {
        public int IdAccesoColocador { get; set; }
        public int CodColocador { get; set; }
        public int CodRazonSocial { get; set; }
        public IEnumerable<PuntosVentaAcceso> CodPuntosVenta { get; set; }
    }
}
