
namespace PlataformaVIA.Services.Interfaces
{
    using PlataformaVIA.Core.Domain;
    using PlataformaVIA.Core.Domain.Documentos;
    using System.Collections.Generic;

    public interface IDocumentosService
    {
        ResponseEO<DocumentoPDF> GetDocumentosPDF(ResponseEO<DocumentoPDF> parametros);
    }
}
