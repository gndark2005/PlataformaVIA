namespace PlataformaVIA.Core.Domain.Media
{
    public class Articulo
    {
        public int Id { get; set; }
        public string NombreImagen { get; set; }
        public int Orden { get; set; }
        public int? CodInstructivo { get; set; }
        public int? CodNoticia { get; set; }
    }
}
