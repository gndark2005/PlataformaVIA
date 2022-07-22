using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
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
    public class AdministradorDocumentosService : IAdministradorDocumentosService
    {
        public IAdministradorDocumentosRepository AdministradorDocumentosRepository { get; }


        public AdministradorDocumentosService(IAdministradorDocumentosRepository administradorDocumentosRepository)
        {
            this.AdministradorDocumentosRepository = administradorDocumentosRepository;
        }

        public IEnumerable<TipoTerminal> GetTiposTerminal()
        {
            return AdministradorDocumentosRepository.GetTiposTerminal();          
        }
        
        public IEnumerable<TiposDocumento> GetTiposDocumento()
        {
            return AdministradorDocumentosRepository.GetTiposDocumento();
        }

        public IEnumerable<Categorias> GetCategorias()
        {
            return AdministradorDocumentosRepository.GetCategorias();
        }

        public int CrearInstructivo(Instructivos model, int idUsuario)
        {
            return AdministradorDocumentosRepository.CrearInstructivo(model, idUsuario);
        }
        

        public int CrearUbicacionInstructivo(InstructivoUbicacion model, int idUsuario)
        {
            return AdministradorDocumentosRepository.CrearUbicacionInstructivo(model, idUsuario);
        }
       
        public ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response)
        {
            return AdministradorDocumentosRepository.GetInstructivos(response);
        }
        
        public ResponseIndividualEO<Instructivos> GetDetallesInstructivo(CriterioBusqueda request)
        {
            return AdministradorDocumentosRepository.GetDetallesInstructivo(request);
        }

        public ResponseEO<ImagenesInstructivo> GetImagenesInstructivo(CriterioBusqueda request)
        {
            return AdministradorDocumentosRepository.GetImagenesInstructivo(request);
        }
        
        public bool ModificarInstructivoImagen(ImagenesInstructivo model, int idUsuario)
        {
            return AdministradorDocumentosRepository.ModificarInstructivoImagen(model, idUsuario);
        }

        public bool ModificarAtributoImagen(ImagenesInstructivo model)
        {
            return AdministradorDocumentosRepository.ModificarAtributoImagen(model);
        }

        public bool ModificarInstructivo(Instructivos model, int idUsuario)
        {
            return AdministradorDocumentosRepository.ModificarInstructivo(model, idUsuario);
        }

        public bool EliminarInstructivo(Instructivos model, int idUsuario)
        {
            return AdministradorDocumentosRepository.EliminarInstructivo(model, idUsuario);
        }
        

        public bool ModificarImagenURL(ImagenesInstructivo model)
        {
            return AdministradorDocumentosRepository.ModificarImagenURL(model);
        }

        public bool CambiarOrdenImagen(ImagenesInstructivo model, int idUsuario, int accion)
        {
            return AdministradorDocumentosRepository.CambiarOrdenImagen(model, idUsuario, accion);
        }

        public bool EliminarImagenInstructivo(ImagenesInstructivo model, int idUsuario)
        {
            return AdministradorDocumentosRepository.EliminarImagenInstructivo(model, idUsuario);
        }

        public bool AgregarInstructivoImagen(ImagenesInstructivo model, int idUsuario)
        {
            return AdministradorDocumentosRepository.AgregarInstructivoImagen(model, idUsuario);
        }

        public IEnumerable<Instructivos> GetAllInstructivoXTipo(int ID_TIPOINSTRUCTIVO)
        {
            return AdministradorDocumentosRepository.GetAllInstructivoXTipo(ID_TIPOINSTRUCTIVO);
        }

    } 
}
