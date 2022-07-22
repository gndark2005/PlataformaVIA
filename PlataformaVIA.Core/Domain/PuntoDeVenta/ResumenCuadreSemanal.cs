namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ResumenCuadreSemanal
    {
        public string Tipo { get; set; }
        //public LineadeNegocioEnum LineaDeNegocio { get; set; }
        [Display(Name = "Línea de Negocio")]
        public string LineaDeNegocio { get; set; }
        public DateTime Fecha { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public int EsPagoMinimo { get; set; }
        public int Id { get; set; }
        public int CodUsuario { get; set; }
        
    }
}
