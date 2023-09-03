using UserServiceAPI.Models.Entities;

namespace UserServiceAPI.Helpers.Filtering
{
    public class FilteringHelper
    {
        public List<UserEntity> FilterUsers(FilteringParameters parameters, List<UserEntity> users)
        {
            if (parameters.FirstName != null)
            {
                users = users.Where(x => x.FirstName.Contains(parameters.FirstName)).ToList();
            }
            if (parameters.LastName != null)
            {
                users = users.Where(x => x.LastName.Contains(parameters.LastName)).ToList();
            }
            if (parameters.Email != null)
            {
                users = users.Where(x => x.Email.Contains(parameters.Email)).ToList();
            }
            if (parameters.PhoneNumber != null)
            {
                users = users.Where(x => x.FirstName.StartsWith(parameters.FirstName)).ToList();
            }
            return users;
        }
    }
}
