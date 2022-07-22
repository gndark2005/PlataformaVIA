namespace PlataformaVIA.Core.Domain.Media
{
    using System;
    using System.Collections.Generic;

    public class Noticia
    {
        public string RutaImagen;
        public DateTime FechaPublicacion;
        IEnumerable<Articulo> Articulos;
    }
}
