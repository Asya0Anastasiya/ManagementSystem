using TimeTrackingService.Data;
using TimeTrackingService.Exceptions;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly IDayAccountingRepository _dayAccountingRepository;

        public DocumentService(IDocumentRepository repository, IDayAccountingRepository dayAccountingRepository)
        {
            _repository = repository;
            _dayAccountingRepository = dayAccountingRepository;
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _repository.AddDocumentAsync(document);
        }

        public async Task<List<string>> GetUsersDocumentsNames(Guid userId, DateTime date)
        {
            return await _repository.GetDocumentsNamesAsync(userId, date);
        }

        public async Task AttachDocumentToDay(string name, DateTime dateTime, Guid userId)
        {
            var document = await _repository.GetUserDocByName(userId, name);
            if (document == null )
            {
                throw new NotFoundException("Document not found");
            }

            var day = await _dayAccountingRepository.CheckDayForExistanceAsync(dateTime, userId);
            if (day == null )
            {
                throw new NotFoundException("No such day");
            }

            document.DaysAccounting.Add(day);

            await _repository.UpdateDocument(document);
        }
    }
}
