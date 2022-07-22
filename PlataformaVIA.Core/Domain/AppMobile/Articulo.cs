namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;

    public class Articulo
    {
        public int Id_Articulo { get; set; }
        public Nullable<int> CodNoticia { get; set; }
        public Nullable<int> CodInstructivo { get; set; }
        public string RutaImagen { get; set; }
        public string RutaCompletaImagen { get; set; }
        public byte[] Imagen { get; set; }
        public int Orden { get; set; }
    }
}
