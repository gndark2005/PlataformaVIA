using PlataformaVIA.Core.Domain;
using PlataformaVIA.Core.Domain.Documentos;
using PlataformaVIA.Data.Repositories.Interfaces;
using PlataformaVIA.Services.Interfaces;
using System.Collections.Generic;


namespace PlataformaVIA.Services.Implementations
{
    public class DocumentosService : IDocumentosService
    {
        public IDocumentosRepository DocumentosRepository { get; }

        public ResponseEO<DocumentoPDF> GetDocumentosPDF(ResponseEO<DocumentoPDF> parametros)
        {
            return DocumentosRepository.GetDocumentosPDF(parametros);
        }

        public DocumentosService(IDocumentosRepository documentosRepository)
        {
            this.DocumentosRepository = documentosRepository;
        }
    }
}
