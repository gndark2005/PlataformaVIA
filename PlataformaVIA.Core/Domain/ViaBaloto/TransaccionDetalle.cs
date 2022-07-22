namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    using System;

    public class TransaccionDetalle
    {
        public string FechaHoraTransaccion { get; set; }
        public int CodigoPuntoDeventa { get; set; }
        public string NombrePuntoDeVenta { get; set; }
        public string FechaReporteAliado { get; set; }
        public string MensajeFrenteTiquete { get; set; }
        public string Referencia1 { get; set; }
        public string Referencia2 { get; set; }
        public string Referencia3 { get; set; }
        public string Referencia4 { get; set; }
        public string Referencia5 { get; set; }
        public string Referencia6 { get; set; }
        public string Referencia7 { get; set; }
        public string Referencia8 { get; set; }
        public string Referencia9 { get; set; }
        public decimal ValorTransaccion { get; set; }
        public string NitAliado { get; set; }
        public string RazonSocialAliado { get; set; }
        public string CanalAtencionUsuario { get; set; }
        public String Categoria { set; get; }
        public string Convenio { set; get; }
        public string NombreProducto { set; get; }
    }
}
