namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ResumenPagosRetirosSemanaActual
    {
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacion { get; set; }
        [Display(Name = "Cuenta Bancaria")]
        public string CuentaBancaria { get; set; }
        [Display(Name = "Fecha Pago")]
        public DateTime FechaPago { get; set; }
        public string Referencia { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
    }
}
