using Microsoft.EntityFrameworkCore;
using UserService.FluentApi;

namespace UserService.Models.Entities
{
    [EntityTypeConfiguration(typeof(AddressConfiguration))]
    public class AddressEntity
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }
    }
}
