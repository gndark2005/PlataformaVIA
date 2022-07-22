namespace PlataformaVIA.Services.Interfaces
{
    using Core.Domain;
    using Core.Domain.Colocador;
    using Core.Domain.General;
    using Core.Domain.PuntoDeVenta;
    using Core.Domain.RepresentanteLegal;
    using PlataformaVIA.Core.Domain.Busqueda;
    using System.Collections.Generic;

    public partial interface IColocadorService
    {
        bool OtorgarAcceso(AccesoColocador accesocolocador);
        ResponseEO<Colocador> GetColocadoresXRazonSocial(ResponseEO<Colocador> response);
        ResponseIndividualEO<Colocador> GetDetalleColocador(CriterioBusqueda request);
        ResponseEO<PuntodeVenta> GetPuntosVentaAccesoColocador(ResponseEO<PuntodeVenta> response);
        IEnumerable<TipoIdentificacion> GetTiposIdentificacion();
        IEnumerable<Ciudad> GetCiudadesByFilter(string filtro);
        IEnumerable<Genero> GetGeneros();
        IEnumerable<TipoSangre> GetTiposSangre();
        IEnumerable<TipoVendedor> GetTiposVendedor();
        int CreateColocador(Colocador colocador, int codUsuario);
        bool EditColocador(Colocador colocador, int codUsuario);
        bool DeleteColocador(CriterioBusqueda request);
        ResponseEO<PuntodeVenta> GetPuntosVentaPorRepresentante(ResponseEO<PuntodeVenta> response);
        bool ChangeAccesoColocador(int codColocador, int codPuntoDeVenta, bool estado, int codUsuario);
        bool OtorgarAccesoGlobalAColocador(int codColocador, int codUsuario);
        bool OtorgarAccesoPorPuntoDeVenta(List<AsignacionPuntoDeVenta> asignaciones, int codUsuario);
        ResponseEO<Colocador> ConsultarColocadoresParaSincronizarSAG(ResponseEO<Colocador> response);
        string ConfirmarSincronizacionConSAG(List<int> ltIdColocadores);
        ResponseEO<Colocador> Colocadores_GetDetalleColocadorXNumeroIdentificacion(Colocador response);
    }
}
