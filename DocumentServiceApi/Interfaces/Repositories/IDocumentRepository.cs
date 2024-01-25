using DocumentServiceApi.Models.Entities;

namespace DocumentServiceApi.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        public Task<bool> IsDocumentExist(Guid documentId);

        public Task<bool> IsDocumentExist(string documentName, Guid userId);

        public Task<List<DocumentEntity>> GetUserDocuments(Guid userId);

        public Task AddDocumentAsync(DocumentEntity documentEntity);

        public Task<DocumentEntity> GetUserDocumentByName(string name, Guid userId);

        public Task<DocumentEntity> GetDocumentById(Guid documentId);
    }
}
