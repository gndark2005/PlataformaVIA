using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class ParametroPaginacion
    {
        //https://www.c-sharpcorner.com/article/how-to-do-paging-with-asp-net-web-api/
        /// <summary>
        /// Número de Registros que se desean mostrar por página
        /// </summary>
        public int TamanoPagina { get; set; } = 10;

        /// <summary>
        /// Índice de la Página inicial de la consulta
        /// </summary>
        public int NumeroPagina { get; set; } = 1;

        /// <summary>
        /// Total de Registros en la Grilla
        /// </summary>
        public int TotalRegistros { get; set; }

    }
}
