using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Entities;

namespace DocumentServiceApi.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        public Task<bool> IsDocumentExist(string fileName, Guid userId);

        public Task<List<DocumentEntity>> GetUserDocuments(Guid userId);

        public Task AddDocumentAsync(DocumentEntity documentEntity);

        public Task<DocumentEntity> GetUserDocumentByName(string name, Guid userId);
    }
}
