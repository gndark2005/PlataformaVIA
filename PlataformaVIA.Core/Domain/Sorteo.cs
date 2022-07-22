using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;

namespace PlataformaVIA.Core.Domain
{
    public class Sorteo
    {
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Int64 Acumulado { set; get; }
        public string B1 { set; get; }
        public string B2 { set; get; }
        public string B3 { set; get; }
        public string B4 { set; get; }
        public string B5 { set; get; }
        public string SB { set; get; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Int64 Revancha { set; get; }
        public string RB1 { set; get; }
        public string RB2 { set; get; }
        public string RB3 { set; get; }
        public string RB4 { set; get; }
        public string RB5 { set; get; }
        public string RSB { set; get; }
        public int NumeroSorteo { set; get; }
        public string  FechaSorteo { set; get; }
        public string TotalGanadoresBaloto { get; set; }
        public string TotalGanadoresRevancha { get; set; }

        public IEnumerable<Instructivos> Banners { set; get; }
    }
}
