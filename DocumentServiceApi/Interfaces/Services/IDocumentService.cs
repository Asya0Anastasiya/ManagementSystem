﻿using DocumentServiceApi.Models.Dto;

namespace DocumentServiceApi.Interfaces.Services
{
    public interface IDocumentService
    {
        public Task<DocumentDto> DownloadDocumentAsync(Guid documentId, Guid userId);

        public Task UploadDocumentAsync(UploadDocument uploadDocument);

        public Task<List<DocumentInfo>> GetUserDocuments(Guid userId);
    }
}
