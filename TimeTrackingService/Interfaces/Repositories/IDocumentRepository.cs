using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDocumentRepository
    { 
        public Task AddDocumentAsync(Document document);

        public Task<List<string>> GetDocumentsNamesAsync(Guid userId, DateTime date);

        public Task<Document> GetUserDocByName(Guid userId, string docName);

        public Task UpdateDocument(Document document);
    }
}
