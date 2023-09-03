using TimeTrackingService.Models.Entities;

namespace TimeTrackingService.Helpers.Filtering
{
    public class FilteringHelper
    {
        public List<DayAccounting> FilterDays(FilteringParameters parameters, List<DayAccounting> days)
        {
            if (parameters.Month != null)
            {
                days = days.Where(x => x.Month == parameters.Month 
                && x.UserId == parameters.UserId).ToList();
            }

            if (parameters.FromDay != null)
            {
                days = days.Where(x => x.Day >= parameters.FromDay
                && x.UserId == parameters.UserId).ToList();
            }

            if (parameters.TillDay != null)
            {
                days = days.Where(x => x.Day <= parameters.TillDay
                && x.UserId == parameters.UserId).ToList();
            }

            if (parameters.AccountingType != null)
            {
                days = days.Where(x => x.AccountingType >= parameters.AccountingType
                && x.UserId == parameters.UserId).ToList();
            }

            return days;
        }
    }
}
