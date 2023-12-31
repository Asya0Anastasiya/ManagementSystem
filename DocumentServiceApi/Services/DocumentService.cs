﻿using AutoMapper;
using DocumentServiceApi.Exceptions;
using DocumentServiceApi.Interfaces.Repositories;
using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Entities;
using DocumentServiceApi.Models.Enums;
using DocumentServiceApi.Models.Messages;
using Google.Cloud.Storage.V1;

namespace DocumentServiceApi.Services
{
    public class DocumentService : IDocumentService 
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _producer;

        public DocumentService(IDocumentRepository repository, IMapper mapper, IMessageProducer producer)
        {
            _repository = repository;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task<DocumentDto> DownloadDocumentAsync(string fileName, Guid userId)
        {
            if (!(await _repository.IsDocumestExist(fileName, userId)))
            {
                throw new NotFoundException("Document not found or you do not have permissions to download it...");
            }
            var client = StorageClient.Create();
            var stream = new MemoryStream();
            var obj = await client.DownloadObjectAsync("test_bucket_asiyar", fileName, stream);
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
            if (await _repository.IsDocumestExist(uploadDocument.File.Name, uploadDocument.UserId))
            {
                throw new InternalException("Document with such name already exist... Please, rename your document");
            }

            using var memoryStream = new MemoryStream();

            await uploadDocument.File.CopyToAsync(memoryStream);

            var client = StorageClient.Create();
            var obj = await client.UploadObjectAsync(
                "test_bucket_asiyar",
                uploadDocument.File.FileName,
                uploadDocument.File.ContentType,
                new MemoryStream(memoryStream.ToArray()));

            var doc = new DocumentEntity()
            {
                Name = obj.Name,
                ContentType = obj.ContentType,
                Size = uploadDocument.File.Length,
                Type = uploadDocument.Type,
                UserId = uploadDocument.UserId
            };

            await _repository.AddDocumentAsync(doc);

            if (doc.Type == Types.TimeTracking)
            {
                DateTime date = new(1990, 1, 1);
                await DocCreatedNotification(doc.Name, date, doc.UserId);
            }
        }

        public async Task<List<DocumentInfo>> GetUserDocuments(Guid userId)
        {
            var documents = await _repository.GetUserDocuments(userId);
            return _mapper.Map<List<DocumentInfo>>(documents);
        }

        public async Task DocCreatedNotification(string name, DateTime date, Guid userId)
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
                Date = date
            };

            var message = new TimeTrackDocumentUploadedMessage()
            {
                DocumentModel = attachDoc
            };

            _producer.SendMessage(message);
        }
    }
}
