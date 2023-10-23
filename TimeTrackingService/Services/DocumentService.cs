using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;

        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _repository.AddDocumentAsync(document);
        }

        public async Task<List<string>> GetUsersDocumentsNames(Guid userId, DateTime date)
        {
            return await _repository.GetDocumentsNamesAsync(userId, date);
        }
    }
}
