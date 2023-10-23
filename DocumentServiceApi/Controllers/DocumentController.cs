using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DocumentServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("downloadUserDocument/{fileName}/{userId}")]
        public async Task<IActionResult> DownloadDocumentAsync([FromRoute] string fileName, Guid userId)
        {
            var obj = await _service.DownloadDocumentAsync(fileName, userId);
            return File(obj.Stream, obj.ContentType, obj.Name);
        }

        [HttpPost]
        [Route("uploadUserDocument")]
        public async Task<IActionResult> UploadDocumentAsync([FromForm] UploadDocument uploadDocument)
        {
            await _service.UploadDocumentAsync(uploadDocument);
            return Ok();
        }

        [HttpGet]
        [Route("getUserDocuments/{userId}")]
        public async Task<List<DocumentInfo>> GetUserDocumentsAsync(Guid userId)
        {
            return await _service.GetUserDocuments(userId);
        }

        [HttpGet]
        [Route("getUserDocumentsNames/{userId}")]
        public async Task<List<string>> GetUserDocumentsNamesAsync(Guid userId)
        {
            return await _service.GetUserDocumentsNames(userId);
        }

        [HttpPost]
        [Route("attachDocument/{userId}")]
        public async Task<IActionResult> AttachDocument([FromRoute] Guid userId, [FromBody] UpcomingDocumentModel upcomingDocumentModel)
        {
            await _service.AttachDocument(upcomingDocumentModel.Name, upcomingDocumentModel.Date, userId);
            return Ok();
        }
    }
}
