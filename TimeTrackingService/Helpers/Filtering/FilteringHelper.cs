using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Helpers.Filtering
{
    public class FilteringHelper
    {
        public List<DayAccounting> FilterDays(FilteringParameters parameters, List<DayAccounting> days)
        {
            if (parameters.FromDate != null)
            {
                days = days.Where(x => x.Date >= parameters.FromDate 
                && x.UserId == parameters.UserId).ToList();
            }

            if (parameters.TillDate != null)
            {
                days = days.Where(x => x.Date <= parameters.TillDate
                && x.UserId == parameters.UserId).ToList();
            }

            if (parameters.AccountingType != null)
            {
                days = days.Where(x => x.AccountingType >= parameters.AccountingType
                && x.UserId == parameters.UserId).ToList();
            }

            return days.OrderBy(day => day.Date).ToList();
        }
    }
}
