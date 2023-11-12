using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

namespace TimeTrackingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("getAttachedUsersTimeTrackingDocumentsNames/{userId}")]
        public async Task<List<string>> GetUsersDocumentsNamesForAdminAsync(Guid userId, [FromQuery] DateTime date)
        {
            return await _documentService.GetAttachedUsersDocumentsNames(userId, date);
        }

        [HttpPost]
        [Route("attachDocument/{userId}")]
        public async Task<IActionResult> AttachDocumentAsync([FromRoute] Guid userId, [FromBody] AttachDocModel attachDocModel)
        {
            await _documentService.AttachDocumentToDay(attachDocModel.Name, attachDocModel.Date, userId);
            return Ok();
        }

        [HttpGet]
        [Route("getAllUsersTimeTrackingDocs/{userId}")]
        public async Task<List<string>> GetAllUsersTimeTrackDocsNamesAsync(Guid userId)
        {
            return await _documentService.GetAllUsersTimeTrackDocsNames(userId);
        }
    }
}
