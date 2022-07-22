namespace PlataformaVIA.Core.Domain.Busqueda
{
    public class CriterioBusqueda
    {
        public int IdPadre { get; set; }
        public int IdUsuario { get; set; }
        public string Filtro { get; set; }
        public ParametroPaginacion Paginacion { get; set; }
    }
}
