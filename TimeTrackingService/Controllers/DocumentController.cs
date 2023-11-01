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
        //change function name in frontend!!!!!!!!!!!!!!!
        [Route("getAttachedUsersTimeTrackingDocumentsNames/{userId}")]
        public async Task<List<string>> GetUsersDocumentsNamesForAdmin(Guid userId, [FromQuery] DateTime date)
        {
            return await _documentService.GetAttachedUsersDocumentsNames(userId, date);
        }

        [HttpPut]
        [Route("attachDocument/{userId}")]
        public async Task<IActionResult> AttachDocument([FromRoute] Guid userId, [FromBody] AttachDocModel attachDocModel)
        {
            await _documentService.AttachDocumentToDay(attachDocModel.Name, attachDocModel.Date, userId);
            return Ok();
        }

        [HttpGet]
        [Route("getAllUsersTimeTrackingDocs/{userId}")]
        public async Task<List<string>> GetAllUsersDocs(Guid userId)
        {
            return await _documentService.GetAllUsersTimeTrackDocsNames(userId);
        }
    }
}
