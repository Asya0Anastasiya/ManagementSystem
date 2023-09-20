using UserService.Models.Entities;

namespace UserService.Helpers.Filtering
{
    public class FilteringHelper
    {
        public IQueryable<UserEntity> FilterUsers(FilteringParameters parameters, IQueryable<UserEntity> users)
        {
            if (parameters == null)
            {
                return users;
            }
            if (parameters.FirstName != null)
            {
                users = users.Where(x => x.FirstName.Contains(parameters.FirstName));
            }
            if (parameters.LastName != null)
            {
                users = users.Where(x => x.LastName.Contains(parameters.LastName));
            }
            if (parameters.Email != null)
            {
                users = users.Where(x => x.Email.Contains(parameters.Email));
            }
            if (parameters.PhoneNumber != null)
            {
                users = users.Where(x => x.PhoneNumber.StartsWith(parameters.PhoneNumber));
            }
            return users;
        }
    }
}
