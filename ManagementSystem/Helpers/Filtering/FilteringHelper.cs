using UserService.Models.Entities;
using UserService.Models.Params;

namespace UserService.Helpers.Filtering
{
    public static class FilteringHelper
    {
        public static IQueryable<UserEntity> FilterUsers(FilteringParameters parameters, IQueryable<UserEntity> users)
        {
            if (parameters == null)
            {
                return users;
            }
            if (!string.IsNullOrEmpty(parameters.FirstName))
            {
                users = users.Where(x => x.FirstName.Contains(parameters.FirstName));
            }
            if (!string.IsNullOrEmpty(parameters.LastName))
            {
                users = users.Where(x => x.LastName.Contains(parameters.LastName));
            }
            if (!string.IsNullOrEmpty(parameters.Email))
            {
                users = users.Where(x => x.Email.Contains(parameters.Email));
            }
            if (!string.IsNullOrEmpty(parameters.PhoneNumber))
            {
                users = users.Where(x => x.PhoneNumber.StartsWith(parameters.PhoneNumber));
            }
            return users;
        }
    }
}
