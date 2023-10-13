﻿using AutoMapper;
using DocumentServiceApi.Exceptions;
using DocumentServiceApi.Interfaces.Repositories;
using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using DocumentServiceApi.Models.Entities;
using Google.Cloud.Storage.V1;

namespace DocumentServiceApi.Services
{
    public class DocumentService : IDocumentService 
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        }

        public async Task<List<DocumentInfo>> GetUserDocuments(Guid userId)
        {
            var documents = await _repository.GetUserDocuments(userId);
            return _mapper.Map<List<DocumentInfo>>(documents);
        }

        public async Task<List<string>> GetUserDocumentsNames(Guid userId)
        {
            return await _repository.GetUserDocumentsNames(userId);
        }
    }
}