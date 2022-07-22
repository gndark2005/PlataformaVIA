namespace PlataformaVIA.Core.Domain.Busqueda
{
    using System;
    public class CriterioBusquedaCicloFacturacion
    {
        public int IdPadre { get; set; }
        public int CodUsuario { get; set; }
        public string Filtro { get; set; }
        public int CodCicloFacturacion { get; set; }
        public ParametroPaginacion Paginacion { get; set; }
    }
}
