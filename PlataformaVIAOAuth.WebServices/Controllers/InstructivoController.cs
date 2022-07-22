namespace PlataformaVIAOAuth.WebServices.Controllers
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Media;
    using PlataformaVIA.Services.Interfaces;
    using System.Collections.Generic;
    using System.Web.Http;

    //[Authorize]
    public class InstructivoController : ApiController
    {
        private IInstructivoService _instructivoService;

        #region Constructores

        public InstructivoController(IInstructivoService instructivoService)
        {
            this._instructivoService = instructivoService;
        }
        #endregion

        public IEnumerable<Instructivo> GetAllInstructivoXTipoTerminal(TipoTerminalEnum tipoTerminal)
        {
            return _instructivoService.GetAllInstructivoXTipoTerminal(tipoTerminal);
        }

        public Instructivo GetInstructivo(int codCategoria, TipoTerminalEnum tipoTerminal)
        {
            //Instructivo objInstructivo = new Instructivo { Id_Instructivo = 1, Descripcion = "Inicio y Cierre de Terminal", CodCategoria = 1, CodTipoTerminal = 1 };
            //return objInstructivo;
            return null;
        }

        public IEnumerable<Articulo> GetArticuloXInstructivo(int codInstructivo)
        {
            //List<Instructivo> instructivos = new List<Instructivo>();
            //List<Articulo> articulosdummy = new List<Articulo>();

            //for (int i = 0; i < 100; i++)
            //{
            //    articulosdummy.Add(new Articulo { Id = i, CodInstructivo = i, NombreImagen = "Baloto" + i + ".png", Orden = 1 });
            //}

            //for (int i = 0; i < 1000; i++)
            //{
            //    instructivos.Add(new Instructivo() { Id = i, Descripcion = "Pruebas " + i, Articulo = articulosdummy });
            //}

            //var articulos = instructivos.Where(mm => mm.Id == codInstructivo).FirstOrDefault().Articulo;

            //return articulos;
            return null;
        }

    }

}
