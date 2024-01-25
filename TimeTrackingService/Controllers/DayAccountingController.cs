using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingService.MediatR.Commands;
using TimeTrackingService.MediatR.Queries;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayAccountingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DayAccountingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("addDay")]
        public async Task<IActionResult> PostDayAsync(CreateDayModel dayModel)
        {
            await _mediator.Send(new AddDayCommand(dayModel));

            return Ok();
        }

        [HttpPost]
        [Route("addDays")]
        public async Task<IActionResult> PostRangeOfDaysAsync([FromBody] List<CreateDayModel> daysAccounting)
        {
            await _mediator.Send(new AddDaysRangeCommand(daysAccounting));

            return Ok();
        }

        [HttpGet]
        [Route("getUsersDays/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetUsersDaysAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var days = await _mediator.Send(new GetUsersDaysQuery(parameters, pageNumber, pageSize));
            
            return Ok(days);
        }

        [HttpGet]
        [Route("getUnconfirmedDaysCount/{userId}")]
        public async Task<IActionResult> GetUnconfirmedDaysCount(Guid userId)
        {
            var count = await _mediator.Send(new GetUnconfirmedDaysCountQuery(userId));

            return Ok(count);
        }

        [HttpDelete("removeDay/{id}")]
        public async Task<IActionResult> RemoveDayAsync(Guid id)
        {
            await _mediator.Send(new RemoveDayCommand(id));

            return Ok();
        }

        //add to ocelot
        [HttpDelete("removeDays")]
        public async Task<IActionResult> RemoveDaysRangeAsync(List<Guid> ids)
        {
            await _mediator.Send(new RemoveDaysRangeCommand(ids));

            return Ok();
        }

        [HttpGet]
        [Route("approveDay/{id}")]
        public async Task<IActionResult> ApproveDayAsync(Guid id)
        {
            await _mediator.Send(new ApproveDayCommand (id));

            return Ok("Successfully approved");
        }

        [HttpGet]
        [Route("getUsersDaysInfo/{userId}/month/{month}/year/{year}")]
        public async Task<UsersDaysModel> GetUsersDaysInfo(Guid userId, int month, int year)
        {
            return await _mediator.Send(new GetUsersDaysInfoQuery(userId, month, year));
        }
    }
}
