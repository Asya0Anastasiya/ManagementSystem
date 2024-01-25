using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.MediatR.Commands;
using TimeTrackingService.MediatR.Queries;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.Controllers
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
        [Route("getAttachedUsersTimeTrackingDocuments/{userId}")]
        public async Task<List<DocumentWithSourceIdModel>> GetUsersDocumentsNamesForAdminAsync(Guid userId, [FromQuery] DateTime date)
        {
            var docs = await _mediator.Send(new GetUsersDocumentsNamesForAdminQuery(userId, date));
            return docs;
        }

        [HttpPost]
        [Route("attachDocument/{userId}")]
        public async Task<IActionResult> AttachDocumentAsync([FromRoute] Guid userId, [FromBody] AttachDocModel attachDocModel)
        {
            await _mediator.Send(new AttachDocumentCommand(userId, attachDocModel));
            return Ok();
        }

        [HttpGet]
        [Route("getAllUsersTimeTrackingDocs/{userId}")]
        public async Task<List<DocumentInfoModel>> GetAllUsersTimeTrackDocsNamesAsync(Guid userId)
        {
            return await _mediator.Send(new GetAllUsersTimeTrackDocsNamesQuery(userId));
        }
    }
}
