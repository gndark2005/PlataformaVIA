namespace PlataformaVIA.Core.Domain.Media
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Instructivo
    {
        [Key]
        public int Id_Instructivo { get; set; }
        public int CodTipoTerminal { get; set; }
        public string TituloPregunta { get; set; }
        public DateTime FechaPublicacion { get; set; }
        //public int CodCategoria { get; set; }
        public List<Articulo> Articulo { get; set; }
    }
}
