using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IDocumentService
    {
        public Task AddDocumentAsync(Document document);

        public Task<List<DocumentWithSourceIdModel>> GetAttachedUsersDocumentsNames(Guid userId, DateTime date);

        public Task<List<DocumentInfoModel>> GetAllUsersTimeTrackDocsNames(Guid userId);

        public Task AttachDocumentToDay(Guid documentId, DateTime dateTime, Guid userId);
    }
}
