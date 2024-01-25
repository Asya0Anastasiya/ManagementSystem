using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDocumentRepository
    { 
        public Task AddDocumentAsync(Document document);

        public Task<List<Document>> GetAttachedDocumentsNamesAsync(Guid userId, DateTime date);

        public Task<List<Document>> GetUserTimeTrackDocsAsync(Guid userId);

        public Task<Document?> GetUserDocByName(Guid userId, string docName);

        public Task<Document?> GetDocumentById(Guid documentId);

        public Task UpdateDocument(Document document);
    }
}
