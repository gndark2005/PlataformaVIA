using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Documentos;
using System.Collections.Generic;

namespace PlataformaVIA.Data.Repositories.Interfaces
{
    public interface IDocumentosRepository
    {
        ResponseEO<DocumentoPDF> GetDocumentosPDF(ResponseEO<DocumentoPDF> parametros);
    }
}
