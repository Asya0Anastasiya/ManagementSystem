using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;

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
            var pagination = new PaginationParameters(pageNumber, pageSize);
            var days = await _service.GetUsersDays(parameters, pagination);
            var metadata = new
            {
                pageSize,
                pageNumber
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(days);
        }

        [HttpGet]
        [Route("getUnconfirmedDaysCount/{id}")]
        public async Task<IActionResult> GetUnconfirmedDaysCount(Guid id)
        {
            var count = await _service.GetUnconfirmedDaysCount(id);
            return Ok(count);
        }

        [HttpDelete("removeDay")]
        public async Task<IActionResult> RemoveDayAsync(Guid id)
        {
            await _service.RemoveDay(id);
            return Ok();
        }

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

        //[HttpGet("workDaysCount")]
        //public int GetWorkDays(Guid id, int month, int year)
        //{
        //    return _service.GetUsersWorkDaysCount(id, month, year);
        //}

        //[HttpGet("sickDaysCount")]
        //public int GetSickDays(Guid id, int month, int year)
        //{
        //    return _service.GetUsersSickDaysCount(id, month, year);
        //}
        
        //[HttpGet("holidaysCount")]
        //public int GetHoliday(Guid id, int month, int year)
        //{
        //    return _service.GetUsersHolidaysCount(id, month, year);
        //}

        //[HttpGet("paidDaysCount")]
        //public int GetPaidDaysCount(Guid id, int month, int year)
        //{
        //    return _service.GetPaidDaysCount(id, month, year);
        //}

        [HttpGet]
        [Route("getUsersDaysInfo/{userId}/month/{month}/year/{year}")]
        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid userId, int month, int year)
        {
            return await _service.GetUsersDaysInfo(userId, month, year);
        }
    }
}
