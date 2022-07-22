namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;
    using System.Collections.Generic;

    public class Noticia
    {
        public int Id_Noticia { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public virtual IEnumerable<Articulo> Articulo { get; set; }
    }
}
