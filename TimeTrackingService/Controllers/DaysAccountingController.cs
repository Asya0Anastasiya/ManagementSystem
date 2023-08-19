using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysAccountingController : ControllerBase
    {
        private readonly IDaysAccountingService service;
        public DaysAccountingController(IDaysAccountingService _service) 
        { 
            service = _service;
        }

        [HttpPost("addDay")]
        public async Task<IActionResult> PostDay([FromBody] DaysAccounting daysAccounting)
        {
            await service.AddDay(daysAccounting);
            return Ok();
        }

        [HttpPost("addDays")]
        public async Task<IActionResult> PostRangeOfDays([FromBody] List<DaysAccounting> daysAccounting)
        {
            await service.AddRangeOfDays(daysAccounting);
            return Ok();
        }

        [HttpGet("getUsersDays")]
        public async Task<List<DaysAccounting>> GetUsersDays(Guid id)
        {
            return await service.GetUsersDays(id);
        }

        [HttpDelete("removeDay")]
        public async Task<IActionResult> RemoveDay(Guid id)
        {
            await service.RemoveDay(id);
            return Ok();
        }

        [HttpDelete("removeDays")]
        public async Task<IActionResult> RemoveDaysRange(List<Guid> ids)
        {
            await service.RemoveRangeOfDays(ids);
            return Ok();
        }
    }
}
