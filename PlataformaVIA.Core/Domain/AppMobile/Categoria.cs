namespace PlataformaVIA.Core.Domain.AppMobile
{
    using System;

    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Habilitado { get; set; }
    }
}
