namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;
    using System.Collections.Generic;

    public class Banner
    {
        public int Id_Banner { get; set; }
        public string titulo { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public virtual IEnumerable<Articulo> Articulo { get; set; }
    }
}
