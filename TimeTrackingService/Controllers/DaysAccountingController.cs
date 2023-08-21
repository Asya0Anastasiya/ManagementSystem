using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
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
        public async Task<IActionResult> PostDay([FromBody] DaysAccountingModel daysAccounting, Guid id)
        {
            await service.AddDay(daysAccounting, id);
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

        [HttpPut("updateDay")]
        public async Task<IActionResult> UpdateDay(DaysAccounting day)
        {
            await service.UpdateDay(day);
            return Ok();
        }

        [HttpPut("approveDay")]
        public async Task<IActionResult> ApproveDay(Guid id)
        {
            await service.ApproveDay(id);
            return Ok("Successfully approved");
        }

        [HttpGet("workDaysCount")]
        public int GetWorkDays(Guid id, int month)
        {
            return service.GetUsersWorkDaysCount(id, month);
        }

        [HttpGet("sickDaysCount")]
        public int GetSickDays(Guid id, int month)
        {
            return service.GetUsersSickDaysCount(id, month);
        }
        
        [HttpGet("holidaysCount")]
        public int GetHoliday(Guid id, int month)
        {
            return service.GetUsersHolidaysCount(id, month);
        }

        [HttpGet("paidDaysCount")]
        public int GetPaidDaysCount(Guid id, int month)
        {
            return service.GetPaidDaysCount(id, month);
        }
    }
}
