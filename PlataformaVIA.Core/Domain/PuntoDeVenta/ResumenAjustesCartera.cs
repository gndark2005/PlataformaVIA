namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ResumenAjustesCartera
    {
        [Display(Name = "Categoría Ajuste")]
        public string CategoriaAjuste { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
        [Display(Name = "Fecha Aplicación")]
        public DateTime FechaAplicacion { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public int Id { get; set; }
        public int CodUsuario { get; set; }
        public int EsPagoMinimo { get; set; }
    }
}
