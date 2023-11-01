using DocumentServiceApi.Models.Dto;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServiceApi.Interfaces.Services
{
    public interface IDocumentService
    {
        public Task<DocumentDto> DownloadDocumentAsync(string fileName, Guid userId);

        public Task UploadDocumentAsync(UploadDocument uploadDocument);

        public Task<List<DocumentInfo>> GetUserDocuments(Guid userId);
    }
}
