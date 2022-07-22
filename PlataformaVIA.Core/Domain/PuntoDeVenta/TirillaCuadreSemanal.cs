namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    using System;

    public class TirillaCuadreSemanal
    {
        /// <summary>
        /// Esta información es según la semana de facturación que se haya seleccionado
        /// </summary>
        public string Periodo { get; set; }
        //public DateTime FechaPeriodoInicial { get; set; }
        //public DateTime FechaPeriodoFinal { get; set; }
        /// <summary>
        /// La fecha que se muestra en este campo es de acuerdo con la política de pago que registra en SFG Cartera para el punto de venta 
        /// </summary>
        public DateTime FechaLimite { get; set; }
        /// <summary>
        /// Corresponde al saldo previo total reportado en SFG cartera y en la tirilla de cuadre semanal, semanas anteriores
        /// </summary>
        public decimal SaldoPrevio { get; set; }
        /// <summary>
        /// Corresponde a la deuda semana actual de IGT reportado en SFG cartera y en la tirilla de cuadre semanal.
        /// </summary>
        public decimal ValorIGT { get; set; }
        /// <summary>
        /// Corresponde a la deuda semana actual de fiducia reportada en SFG cartera y en la tirilla de cuadre semanal
        /// </summary>
        public decimal ValorFiducia { get; set; }
        /// <summary>
        /// Es la sumatoria del saldo previo, el valor IGT y valor fiducia.
        /// </summary>
        public decimal Total { get; set; }

        public string RutaArchivo { get; set; }
    }
}
