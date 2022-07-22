namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System.ComponentModel.DataAnnotations;

    public class PresupuestoXLineaDeNegocio
    {
        [Display(Name = "Línea de Negocio")]
        public string LineaDeNegocio { get; set; }/*LineadeNegocioEnum*/
        [Display(Name = "Presupuesto Ventas")]
        public decimal Presupuesto { get; set; }
        [Display(Name = "Presupuesto Ventas")]
        public decimal PresupuestoCumplido { get; set; }
        [Display(Name = "Tipo de analisis linea de negocio")]
        public byte TipoAnalisis { get; set; }

        public int PorcentajePresupuesto { get {
                int porcentaje = 0;
                if (this.Presupuesto > 0 && this.PresupuestoCumplido >0) {
                    porcentaje = (int)((this.PresupuestoCumplido * 100) / this.Presupuesto);
                }

                if (porcentaje > 100) {
                    porcentaje = 100;
                }
                return porcentaje;
            } }
    }
}
