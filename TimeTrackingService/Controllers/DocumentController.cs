using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Interfaces.Services;

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
        [Route("getUsersTimeTrackingDocumentsNames/{userId}")]
        public async Task<List<string>> GetUsersDocumentsNames(Guid userId, [FromQuery] DateTime date)
        {
            return await _documentService.GetUsersDocumentsNames(userId, date);
        }
    }
}
