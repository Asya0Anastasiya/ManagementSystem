using AutoMapper;
using TimeTrackingService.Exceptions;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly IDayAccountingRepository _dayAccountingRepository;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository repository, 
                                IDayAccountingRepository dayAccountingRepository, 
                                IMapper mapper)
        {
            _repository = repository;
            _dayAccountingRepository = dayAccountingRepository;
            _mapper = mapper;
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _repository.AddDocumentAsync(document);
        }

        public async Task<List<DocumentWithSourceIdModel>> GetAttachedUsersDocumentsNames(Guid userId, DateTime date)
        {
            var documents = await _repository.GetAttachedDocumentsNamesAsync(userId, date);

            return _mapper.Map<List<DocumentWithSourceIdModel>>(documents);
        }

        public async Task AttachDocumentToDay(Guid documentId, DateTime dateTime, Guid userId)
        {
            var document = await _repository.GetDocumentById(documentId);

            if (document == null )
            {
                throw new NotFoundException("Document not found");
            }

            var day = await _dayAccountingRepository.CheckDayForExistenceAsync(dateTime, userId);

            if (day == null )
            {
                throw new NotFoundException("Day not found");
            }

            document.DaysAccounting.Add(day);

            await _repository.UpdateDocument(document);
        }

        public async Task<List<DocumentInfoModel>> GetAllUsersTimeTrackDocsNames(Guid userId)
        {
            var documents = await _repository.GetUserTimeTrackDocsAsync(userId);

            return _mapper.Map<List<DocumentInfoModel>>(documents);
        }
    }
}
