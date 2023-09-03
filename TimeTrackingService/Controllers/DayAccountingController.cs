﻿using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.Helpers.Filtering;
using TimeTrackingService.Helpers.Pagination;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

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

        [HttpPost("addDay")]
        public async Task<IActionResult> PostDayAsync([FromBody] DayAccountingModel dayAccounting, Guid id)
        {
            await _service.AddDay(dayAccounting, id);
            return Ok();
        }

        [HttpPost("addDays")]
        public async Task<IActionResult> PostRangeOfDaysAsync([FromBody] List<DayAccountingModel> daysAccounting, Guid id)
        {
            await _service.AddRangeOfDays(daysAccounting, id);
            return Ok();
        }

        [HttpGet]
        [Route("getUsersDays/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<List<DayAccountingModel>> GetUsersDaysAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var pagination = new PaginationParameters(pageNumber, pageSize);
            return await _service.GetUsersDays(parameters, pagination);
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

        [HttpPut("updateDay")]
        public async Task<IActionResult> UpdateDayAsync(DayAccounting day)
        {
            await _service.UpdateDay(day);
            return Ok();
        }

        [HttpPut]
        [Route("approveDay")]
        public async Task<IActionResult> ApproveDayAsync([FromBody] DayAccounting dayAccounting)
        {
            await _service.ApproveDay(dayAccounting.Id);
            return Ok("Successfully approved");
        }

        [HttpGet("workDaysCount")]
        public int GetWorkDays(Guid id, int month, int year)
        {
            return _service.GetUsersWorkDaysCount(id, month, year);
        }

        [HttpGet("sickDaysCount")]
        public int GetSickDays(Guid id, int month, int year)
        {
            return _service.GetUsersSickDaysCount(id, month, year);
        }
        
        [HttpGet("holidaysCount")]
        public int GetHoliday(Guid id, int month, int year)
        {
            return _service.GetUsersHolidaysCount(id, month, year);
        }

        [HttpGet("paidDaysCount")]
        public int GetPaidDaysCount(Guid id, int month, int year)
        {
            return _service.GetPaidDaysCount(id, month, year);
        }

        [HttpGet]
        [Route("getUsersDaysInfo/{userId}/month/{month}/year/{year}")]
        public UsersDaysModel GetUsersDaysInfo(Guid userId, int month, int year)
        {
            return _service.GetUsersDaysInfo(userId, month, year);
        }
    }
}
