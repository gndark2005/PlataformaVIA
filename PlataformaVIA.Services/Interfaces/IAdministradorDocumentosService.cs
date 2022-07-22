using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
using PlataformaVIA.Core.Domain.Busqueda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaVIA.Services.Interfaces
{
    public interface IAdministradorDocumentosService
    {
        IEnumerable<TipoTerminal> GetTiposTerminal();
        IEnumerable<TiposDocumento> GetTiposDocumento();
        int CrearInstructivo(Instructivos model, int idUsuario);
        int CrearUbicacionInstructivo(InstructivoUbicacion model, int idUsuario);
        ResponseEO<Instructivos> GetInstructivos(ResponseEO<Instructivos> response);
        ResponseIndividualEO<Instructivos> GetDetallesInstructivo(CriterioBusqueda request);
        ResponseEO<ImagenesInstructivo> GetImagenesInstructivo(CriterioBusqueda request);
        bool ModificarInstructivoImagen(ImagenesInstructivo model, int idUsuario);
        bool ModificarAtributoImagen(ImagenesInstructivo model);
        bool ModificarImagenURL(ImagenesInstructivo model);
        bool ModificarInstructivo(Instructivos model, int idUsuario);
        bool EliminarInstructivo(Instructivos model, int idUsuario);
        bool CambiarOrdenImagen(ImagenesInstructivo model, int idUsuario, int accion);
        bool EliminarImagenInstructivo(ImagenesInstructivo model, int idUsuario);
        bool AgregarInstructivoImagen(ImagenesInstructivo model, int idUsuario);
        IEnumerable<Categorias> GetCategorias();
        IEnumerable<Instructivos> GetAllInstructivoXTipo(int ID_TIPOINSTRUCTIVO);
    }
}
