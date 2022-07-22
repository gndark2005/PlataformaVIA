using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.ViaBaloto
{
    public class Ciudad
    {
        public int Id_Ciudad { get; set; }
        [Display(Name="Nombre de la ciudad")]
        public string NombreCiudad { get; set; }
        [Display(Name = "Nombre del departamento")]
        public string NombreDepartamento { get; set; }
    }
}

