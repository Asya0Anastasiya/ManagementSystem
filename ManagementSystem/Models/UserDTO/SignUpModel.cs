﻿using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models.UserModels
{
    public class SignUpModel
    {
        //[Required]
        public string Firstname { get; set; }

        //[Required]
        public string Lastname { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string Password { get; set; }
    }
}
