using TimeTrackingService.Models.Entities;
using UserServiceAPI.Clients;
using UserServiceAPI.Interfaces.Repositories;

namespace UserServiceAPI.Repositories
{
    public class DaysAccountingClientRepository : IDaysAccountingClientRepository
    {
        private readonly IDayAccountingClient client;
        public DaysAccountingClientRepository(IDayAccountingClient _client)
        {
            client = _client;
        }

        public async Task<List<DayAccounting>> GetDays(Guid id)
        {
            return await client.GetDays(id);
        }

        public async Task<int> GetHolidaysCount(Guid id, int month, int year)
        {
            return await client.GetHolidaysCount(id, month, year);
        }

        public async Task<int> GetPaidDaysCount(Guid id, int month, int year)
        {
            return await client.GetPaidDaysCount(id, month, year);
        }

        public async Task<int> GetSickDaysCount(Guid id, int month, int year)
        {
            return await client.GetSickDaysCount(id, month, year);
        }

        public async Task<int> GetWorkDaysCount(Guid id, int month, int year)
        {
            return await client.GetWorkDaysCount(id, month, year);
        }
    }
}
