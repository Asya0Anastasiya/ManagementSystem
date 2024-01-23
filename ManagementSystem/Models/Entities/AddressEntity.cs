﻿namespace UserService.Models.Entities
{
    public class AddressEntity {

        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }
    }
}
