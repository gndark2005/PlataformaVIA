using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Busqueda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Services.Interfaces
{
    public interface IDivulgacionService
    {
        int ActualizarDivulgacion(Divulgacion model);

        int AgregarDivulgacion(Divulgacion model);

        ResponseEO<Divulgacion> GetDivulgaciones(ResponseEO<Divulgacion> response);

        ResponseIndividualEO<Divulgacion> GetDetallesDivulgacion(CriterioBusqueda request);
        IEnumerable<Rol> GetRoles();

        int AgregarDivulgacionxRol(DivulgacionxRol model);

        IEnumerable<DivulgacionxRol> ObtenerRolesxDivulgacion(int idDiv);

        int EliminarRolesxDivulgacion(int idDiv);

        IEnumerable<Divulgacion> ObtenerDivulgacionxUsuario(int idUser);

        int InsertarRespuestaDivulgacion(Divulgacion model);

        IEnumerable<ResultadoDivulgacion> ObtenerResultadosDivulgacion(int idDivulgacion, DateTime fechaInicio, DateTime fechaFin);

        IEnumerable<NitDivulgacion> ObtenerNITS(string search);

        int AgregarExcepcionxNIT(ExcepcionxNIT model);

        IEnumerable<ExcepcionxNIT> ObtenerExcepcionesXDivulgacion(int idDiv);

        int EliminarExcepcionesxDivulgacion(int idDiv);

        IEnumerable<ExcepcionxNIT> ValidarNitxDivulgacion(ExcepcionxNIT model);
    }
}
