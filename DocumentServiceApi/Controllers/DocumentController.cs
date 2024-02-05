using DocumentServiceApi.MediatR.Commands;
using DocumentServiceApi.MediatR.Queries;
using DocumentServiceApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("downloadUserDocument/{documentId}/{userId}")]
        public async Task<IActionResult> DownloadDocumentAsync([FromRoute] Guid documentId, Guid userId)
        {
            var obj = await _mediator.Send(new DownloadDocumentQuery(userId, documentId));
            return File(obj.Stream, obj.ContentType, obj.Name);
        }

        [HttpPost]
        [Route("uploadUserDocument")]
        public async Task<IActionResult> UploadDocumentAsync([FromForm] UploadDocument uploadDocument)
        {
            await _mediator.Send(new UploadDocumentCommand(uploadDocument));
            return Ok();
        }

        [HttpGet]
        [Route("getUserDocuments/{userId}")]
        public async Task<List<DocumentInfo>> GetUserDocumentsAsync(Guid userId)
        {
            return await _mediator.Send(new GetUserDocumentsQuery(userId));
        }
    }
}
