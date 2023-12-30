using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayAccountingController : ControllerBase
    {
        private readonly IDayAccountingService _service;

        public DayAccountingController(IDayAccountingService service) 
        { 
            _service = service;
        }

        [HttpPost]
        [Route("addDay")]
        public async Task<IActionResult> PostDayAsync(CreateDayModel dayModel)
        {
            await _service.AddDay(dayModel);

            return Ok();
        }

        [HttpPost]
        [Route("addDays")]
        public async Task<IActionResult> PostRangeOfDaysAsync([FromBody] List<CreateDayModel> daysAccounting)
        {
            await _service.AddRangeOfDays(daysAccounting);

            return Ok();
        }

        [HttpGet]
        [Route("getUsersDays/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetUsersDaysAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var days = await _service.GetUsersDays(parameters, pageNumber, pageSize);
            
            return Ok(days);
        }

        [HttpGet]
        [Route("getUnconfirmedDaysCount/{userId}")]
        public async Task<IActionResult> GetUnconfirmedDaysCount(Guid userId)
        {
            var count = await _service.GetUnconfirmedDaysCount(userId);

            return Ok(count);
        }

        [HttpDelete("removeDay/{id}")]
        public async Task<IActionResult> RemoveDayAsync(Guid id)
        {
            await _service.RemoveDayAsync(id);

            return Ok();
        }

        //add to ocelot
        [HttpDelete("removeDays")]
        public async Task<IActionResult> RemoveDaysRangeAsync(List<Guid> ids)
        {
            await _service.RemoveRangeOfDays(ids);

            return Ok();
        }

        [HttpGet]
        [Route("approveDay/{id}")]
        public async Task<IActionResult> ApproveDayAsync(Guid id)
        {
            await _service.ApproveDayAsync(id);

            return Ok("Successfully approved");
        }

        [HttpGet]
        [Route("getUsersDaysInfo/{userId}/month/{month}/year/{year}")]
        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid userId, int month, int year)
        {
            return await _service.GetUsersDaysInfo(userId, month, year);
        }
    }
}
