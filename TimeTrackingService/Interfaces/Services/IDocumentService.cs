using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDocumentService
    {
        public Task AddDocumentAsync(Document document);

        public Task<List<string>> GetAttachedUsersDocumentsNames(Guid userId, DateTime date);

        public Task<List<string>> GetAllUsersTimeTrackDocsNames(Guid userId);

        public Task AttachDocumentToDay(string name, DateTime dateTime, Guid userId);
    }
}
