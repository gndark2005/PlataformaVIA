namespace PlataformaVIA.Core.Domain.PuntoDeVenta
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string LineaDeNegocio { get; set; }
        public string Aliado { get; set; }
        public int EsSubproducto { get; set; }
    }
}
