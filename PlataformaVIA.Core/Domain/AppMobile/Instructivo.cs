namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;
    using System.Collections.Generic;

    public class Instructivo
    {
        public int Id_Instructivo { get; set; }
        public int CodTipoTerminal { get; set; }
        public int CodCategoria { get; set; }
        public string TituloPregunta { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool Habilitado { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public virtual IEnumerable<Articulo> Articulo { get; set; }
        public virtual int TipoTerminal { get; set; }
    }
}
