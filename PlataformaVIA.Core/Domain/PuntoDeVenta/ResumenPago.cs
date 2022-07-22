namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System.ComponentModel.DataAnnotations;

    public class ResumenPago
    {
        //public decimal Liquidacion { get; set; }
        //public decimal Ventas { get; set; }
        //public decimal AjustesFacturacion { get; set; }
        //public decimal Premios { get; set; }
        //public decimal Retiros { get; set; }
        //public decimal Pagos { get; set; }
        //public decimal AjustesCartera { get; set; }
        //public decimal ValorTotal { get; set; }
        public string Concepto { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        //public int idResumen
        public int Id { get; set; }
        public int CodUsuario { get; set; }
        public int EsPagoMinimo { get; set; }
    }
}
