namespace PlataformaVIA.Presentacion.ViewModels
{
    public class BarcodeViewModel
    {
        public string TextoBarcodeIGT { get; set; }
        public string TextoBarcodeFiducia { get; set; }
        public byte[] BarcodeIGT { get; set; }
        public byte[] BarcodeFiducia { get; set; }
    }
}