namespace PlataformaVIA.Core.Domain
{
    using System;

    public class CicloFacturacion
    {
        public int Id { get; set; }
        public int CodCiclo { get; set; }
        public int Secuencia { get; set; }
        public string RangoFechaFacturacion { get; set; }
        public string RangoFechaDepositos { get; set; }
        public string RangoParaReportesDiarios { get; set; }
    }
}
