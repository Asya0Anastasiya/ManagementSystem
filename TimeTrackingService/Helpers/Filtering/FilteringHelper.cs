using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Params;

namespace TimeTrackingService.Helpers.Filtering
{
    public static class FilteringHelper
    {
        public static IQueryable<DayAccounting> FilterDays(FilteringParameters parameters, IQueryable<DayAccounting> days)
        {
            if (parameters.FromDate != null)
            {
                days = days.Where(x => x.Date >= parameters.FromDate 
                && x.UserId == parameters.UserId);
            }

            if (parameters.TillDate != null)
            {
                days = days.Where(x => x.Date <= parameters.TillDate
                && x.UserId == parameters.UserId);
            }

            if (parameters.AccountingType != null)
            {
                days = days.Where(x => x.AccountingType >= parameters.AccountingType
                && x.UserId == parameters.UserId);
            }

            return days.OrderBy(day => day.Date);
        }
    }
}
