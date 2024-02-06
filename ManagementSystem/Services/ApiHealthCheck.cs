using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UserService.Services
{
    //public class ApiHealthCheck : IHealthCheck
    //{
    //    private readonly IUserRepository _userRepository;

    //    public ApiHealthCheck(IUserRepository userRepository)
    //    {
    //        _userRepository = userRepository;
    //    }

    //    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
    //                                                CancellationToken cancellationToken = default)
    //    {
    //        try
    //        {
    //            await _userRepository.GetUsersAsync(null, new PaginationParameters(1, 1));
    //            return await Task.FromResult(new HealthCheckResult(
    //                  status: HealthStatus.Healthy,
    //                  description: "The API is up and running."));
    //        }
    //        catch (Exception)
    //        {
    //            return await Task.FromResult(new HealthCheckResult(
    //              status: HealthStatus.Unhealthy,
    //              description: "The API is down."));
    //        }
    //    }
    //}

    public class ApiHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync
        (HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44307/api/User/getUsers/pageNumber/1/pageSize/1");

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is up and running."));
                }

                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "The API is down."));
            }
        }
    }
}