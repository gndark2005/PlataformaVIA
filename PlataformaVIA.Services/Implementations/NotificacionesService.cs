using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;

namespace PlataformaVIA.Services.Implementations
{
    public class NotificacionesService : INotificacionesService
    {
        public INotificacionesRepository NotificacionesRepository { get; }


        public NotificacionesService(INotificacionesRepository NotificacionesRepository)
        {
            this.NotificacionesRepository = NotificacionesRepository;
        }

        public ResponseEO<Notificaciones> GetNotificaciones(int IdUsuario)
        {
            return this.NotificacionesRepository.GetNotificaciones(IdUsuario);
        }

        public int CrearTokenPorUsuario(Notificaciones request)
        {
            return this.NotificacionesRepository.CrearTokenPorUsuario(request);
        }

        public ResponseEO<Notificaciones> CrearNotificacion(Notificaciones request)
        {
            return this.NotificacionesRepository.CrearNotificacion(request);
        }

        public int ActualizarNotificacion(Notificaciones request)
        {
            return this.NotificacionesRepository.ActualizarNotificacion(request);
        }
    }
}
