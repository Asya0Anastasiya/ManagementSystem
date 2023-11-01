using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Repositories
{
    public interface IDocumentRepository
    { 
        public Task AddDocumentAsync(Document document);

        public Task<List<string>> GetAttachedDocumentsNamesAsync(Guid userId, DateTime date);

        public Task<List<string>> GetAllUsersTimeTrackDocsNames(Guid userId);

        public Task<Document> GetUserDocByName(Guid userId, string docName);

        public Task UpdateDocument(Document document);
    }
}
