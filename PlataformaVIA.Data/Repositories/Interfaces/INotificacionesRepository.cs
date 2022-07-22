using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface INotificacionesRepository
    {
        ResponseEO<Notificaciones> GetNotificaciones(int idUsuario);
        int CrearTokenPorUsuario(Notificaciones request);
        ResponseEO<Notificaciones> CrearNotificacion(Notificaciones request);
        int ActualizarNotificacion(Notificaciones request);

    }
}
