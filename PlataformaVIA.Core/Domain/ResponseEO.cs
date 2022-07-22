namespace PlataformaVIA.Core.Domain
{
    using PlataformaVIA.Core.Domain.Busqueda;
    using System.Collections.Generic;

    public class ResponseEO<T>
    {
        public CriterioBusquedaFechas FiltrosFechas { get; set; }
        public CriterioBusquedaCicloFacturacion FiltrosCicloFacturacion { get; set; }
        public CriterioBusqueda FiltrosCriterio { get; set; }
        public BusquedaDetallePago DetallesPago { get; set; }
        public BusquedaDetalleAjuste DetallesAjuste { get; set; }
        public int IdUsuario { get; set; }
        public string TextoBusqueda { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public IEnumerable<T> Entidades { get; set; }
        public Message Mensaje { get; set; }

        public ResponseEO()
        {
            Mensaje = new Message();
            Mensaje.Error = false;
            Mensaje.ErrorMessage = string.Empty;
        }
    }
}
