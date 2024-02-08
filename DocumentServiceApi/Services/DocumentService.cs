using AutoMapper;
using DocumentServiceApi.Exceptions;
using DocumentServiceApi.Interfaces.Repositories;
using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Entities;
using DocumentServiceApi.Models.Enums;
using DocumentServiceApi.Models.Messages;
using DocumentServiceApi.Options;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace DocumentServiceApi.Services
{
    public class DocumentService : IDocumentService 
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _producer;
        private readonly BucketOptions _bucketOptions;

        private const int BytesCount = 1024;

        public DocumentService(IDocumentRepository repository, IMapper mapper, 
                                IMessageProducer producer, IOptions<BucketOptions> bucketOptions)
        {
            _repository = repository;
            _mapper = mapper;
            _producer = producer;
            _bucketOptions = bucketOptions.Value;
        }

        public async Task<DocumentDto> DownloadDocumentAsync(Guid documentId, Guid userId)
        {
            var doc = await _repository.GetDocumentById(documentId);

            if (doc == null)
            {
                throw new NotFoundException("Document not found or you do not have permissions to download it...");
            }

            var client = StorageClient.Create();

            var stream = new MemoryStream();
            var obj = await client.DownloadObjectAsync(_bucketOptions.BucketName, doc.Name, stream);
            stream.Position = 0;

            var document = new DocumentDto()
            {
                Name = obj.Name,
                ContentType = obj.ContentType,
                Stream = stream
            };

            return document;
        }

        public async Task UploadDocumentAsync(UploadDocument uploadDocument)
        {
            if (await _repository.IsDocumentExist(uploadDocument.File.Name, uploadDocument.UserId))
            {
                throw new InternalException("Document with such name already exist... Please, rename your document");
            }

            using var memoryStream = new MemoryStream();

            await uploadDocument.File.CopyToAsync(memoryStream);

            var client = StorageClient.Create();

            var obj = await client.UploadObjectAsync(
                _bucketOptions.BucketName,
                uploadDocument.File.FileName,
                uploadDocument.File.ContentType,
                new MemoryStream(memoryStream.ToArray()));

            var document = new DocumentEntity()
            {
                Name = obj.Name,
                ContentType = obj.ContentType,
                Size = ConvertBytesToKiloBytes(uploadDocument.File.Length),
                Type = uploadDocument.Type,
                UploadDate = DateTime.Now,
                UserId = uploadDocument.UserId
            };

            await _repository.AddDocumentAsync(document);

            if (document.Type == Types.TimeTracking)
            {
                await DocCreatedNotification(document.Name, document.UserId);
            }
        }

        public async Task<List<DocumentInfo>> GetUserDocuments(Guid userId)
        {
            var documents = await _repository.GetUserDocuments(userId);

            return _mapper.Map<List<DocumentInfo>>(documents);
        }

        private async Task DocCreatedNotification(string name, Guid userId)
        {
            var document = await _repository.GetUserDocumentByName(name, userId);

            if (document == null)
            {
                throw new NotFoundException("There is no such document");
            }

            var attachDoc = new AttachDocumentModel()
            {
                Name = document.Name,
                Type = document.Type,
                UserId = userId,
                SourceId = document.Id,
            };

            var message = new TimeTrackDocumentUploadedMessage()
            {
                DocumentModel = attachDoc
            };

            _producer.SendMessage(message);
        }

        private double ConvertBytesToKiloBytes(long fileSize)
        {
            return fileSize / BytesCount;
        }
    }
}
