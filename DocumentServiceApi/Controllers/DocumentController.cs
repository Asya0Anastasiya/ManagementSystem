using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

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
    }
}
