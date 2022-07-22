namespace PlataformaVIA.Services.Implementations
{
    using Core.Domain;
    using Core.Domain.Colocador;
    using Core.Domain.General;
    using Core.Domain.PuntoDeVenta;
    using Core.Domain.RepresentanteLegal;
    using Data.Repositories.Interfaces;
    using PlataformaVIA.Core.Domain.Busqueda;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class ColocadorService : IColocadorService
    {
        public IColocadorRepository ColocadorRepository { get; }


        public ColocadorService(IColocadorRepository colocadorRepository)
        {
            this.ColocadorRepository = colocadorRepository;
        }

        public ResponseEO<Colocador> GetColocadoresXRazonSocial(ResponseEO<Colocador> response)
        {
            return ColocadorRepository.GetColocadoresXRazonSocial(response);
        }

        public bool OtorgarAcceso(AccesoColocador accesocolocador)
        {
            return ColocadorRepository.OtorgarAcceso(accesocolocador);
        }

        public ResponseIndividualEO<Colocador> GetDetalleColocador(CriterioBusqueda request)
        {
            return ColocadorRepository.GetDetalleColocador(request);
        }

        public ResponseEO<PuntodeVenta> GetPuntosVentaAccesoColocador(ResponseEO<PuntodeVenta> response)
        {
            return ColocadorRepository.GetPuntosVentaAccesoColocador(response);
        }

        public IEnumerable<TipoIdentificacion> GetTiposIdentificacion() {
            return ColocadorRepository.GetTiposIdentificacion();
        }

        public IEnumerable<Ciudad> GetCiudadesByFilter(string filtro) {
            return ColocadorRepository.GetCiudadesByFilter(filtro);
        }

        public IEnumerable<Genero> GetGeneros() {
            return ColocadorRepository.GetGeneros();
        }

        public IEnumerable<TipoSangre> GetTiposSangre() {
            return ColocadorRepository.GetTiposSangre();
        }

        public IEnumerable<TipoVendedor> GetTiposVendedor() {
            return ColocadorRepository.GetTiposVendedor();
        }

        public int CreateColocador(Colocador colocador, int codUsuario) {
            return ColocadorRepository.CreateColocador(colocador, codUsuario);
        }

        public bool EditColocador(Colocador colocador, int codUsuario)
        {
            return ColocadorRepository.EditColocador(colocador, codUsuario);
        }

        public bool DeleteColocador(CriterioBusqueda request) {
            return ColocadorRepository.DeleteColocador(request);
        }

        public ResponseEO<PuntodeVenta> GetPuntosVentaPorRepresentante(ResponseEO<PuntodeVenta> response) {
            return ColocadorRepository.GetPuntosVentaPorRepresentante(response);
        }

        public bool ChangeAccesoColocador(int codColocador, int codPuntoDeVenta, bool estado, int codUsuario) {
            return ColocadorRepository.ChangeAccesoColocador(codColocador, codPuntoDeVenta, estado, codUsuario);
        }

        public bool OtorgarAccesoGlobalAColocador(int codColocador, int codUsuario) {
            return ColocadorRepository.OtorgarAccesoGlobalAColocador(codColocador, codUsuario);
        }

        public bool OtorgarAccesoPorPuntoDeVenta(List<AsignacionPuntoDeVenta> asignaciones, int codUsuario) {
            return ColocadorRepository.OtorgarAccesoPorPuntoDeVenta(asignaciones, codUsuario);
        }

        public ResponseEO<Colocador> ConsultarColocadoresParaSincronizarSAG(ResponseEO<Colocador> response) {
            return ColocadorRepository.ConsultarColocadoresParaSincronizarSAG(response);
        }

        public string ConfirmarSincronizacionConSAG(List<int> ltIdColocadores)
        {
            return ColocadorRepository.ConfirmarSincronizacionConSAG(ltIdColocadores);
        }

        public ResponseEO<Colocador> Colocadores_GetDetalleColocadorXNumeroIdentificacion(Colocador response)
        {
            return ColocadorRepository.Colocadores_GetDetalleColocadorXNumeroIdentificacion(response);
        }
    }
}
