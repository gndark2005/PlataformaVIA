namespace PlataformaVIA.Core.Domain
{
    public class ProductoComercial
    {
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public LineadeNegocioEnum LineaNegocio { get; set; }
        public string AliadoOBanco { get; set; }
    }
}
