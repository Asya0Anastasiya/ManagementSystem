using Microsoft.Extensions.Diagnostics.HealthChecks;
using UserService.Helpers.Pagination;
using UserService.Interfaces.Repositories;

namespace UserService.Services
{
    public class ApiHealthCheck : IHealthCheck
    {
        private readonly IUserRepository _userRepository;

        public ApiHealthCheck(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
                                                    CancellationToken cancellationToken = default)
        {
            try
            {
                await _userRepository.GetUsersAsync(null, new PaginationParameters(1, 1));
                return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is up and running."));
            }
            catch (Exception)
            {
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "The API is down."));
            }
        }
    }
}
