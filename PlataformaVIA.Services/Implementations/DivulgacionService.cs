using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Busqueda;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Services.Implementations
{
    public class DivulgacionService : IDivulgacionService
    {
        public IDivulgacionRepository DivulgacionRepository { get; }

        public DivulgacionService(IDivulgacionRepository DivulgacionRepository)
        {
            this.DivulgacionRepository = DivulgacionRepository;
        }

        public int ActualizarDivulgacion(Divulgacion model)
        {
            return this.DivulgacionRepository.ActualizarDivulgacion(model);
        }

        public int AgregarDivulgacion(Divulgacion model)
        {
            return this.DivulgacionRepository.AgregarDivulgacion(model);
        }

        public ResponseEO<Divulgacion> GetDivulgaciones(ResponseEO<Divulgacion> response)
        {
            return this.DivulgacionRepository.GetDivulgaciones(response);
        }

        public ResponseIndividualEO<Divulgacion> GetDetallesDivulgacion(CriterioBusqueda request)
        {
            return this.DivulgacionRepository.GetDetallesDivulgacion(request);
        }

       
         public IEnumerable<Rol> GetRoles()
        {
            return this.DivulgacionRepository.GetRoles();
        }

        public int AgregarDivulgacionxRol(DivulgacionxRol model)
        {
            return this.DivulgacionRepository.AgregarDivulgacionxRol(model);
        }

        public IEnumerable<DivulgacionxRol> ObtenerRolesxDivulgacion(int idDiv)
        {
            return this.DivulgacionRepository.ObtenerRolesxDivulgacion(idDiv);
        }
        
        public int EliminarRolesxDivulgacion(int idDiv)
        {
            return this.DivulgacionRepository.EliminarRolesxDivulgacion(idDiv);
        }

        public IEnumerable<Divulgacion> ObtenerDivulgacionxUsuario(int idUser)
        {
            return this.DivulgacionRepository.ObtenerDivulgacionxUsuario(idUser);
        }

        public int InsertarRespuestaDivulgacion(Divulgacion model)
        {
            return this.DivulgacionRepository.InsertarRespuestaDivulgacion(model);
        }

        public IEnumerable<ResultadoDivulgacion> ObtenerResultadosDivulgacion(int idDivulgacion, DateTime fechaInicio, DateTime fechaFin)
        {
            return this.DivulgacionRepository.ObtenerResultadosDivulgacion(idDivulgacion, fechaInicio, fechaFin);
        }

        public IEnumerable<NitDivulgacion> ObtenerNITS(string search)
        {
            return this.DivulgacionRepository.ObtenerNITS(search);
        }

        public int AgregarExcepcionxNIT(ExcepcionxNIT model)
        {
            return this.DivulgacionRepository.AgregarExcepcionxNIT(model);
        }

        public IEnumerable<ExcepcionxNIT> ObtenerExcepcionesXDivulgacion(int idDiv)
        {
            return this.DivulgacionRepository.ObtenerExcepcionesXDivulgacion(idDiv);
        }

        public int EliminarExcepcionesxDivulgacion(int idDiv)
        {
            return this.DivulgacionRepository.EliminarExcepcionesxDivulgacion(idDiv);
        }

        public IEnumerable<ExcepcionxNIT> ValidarNitxDivulgacion(ExcepcionxNIT model)
        {
            return this.DivulgacionRepository.ValidarNitxDivulgacion(model);
        }

    }
}
