﻿using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models.UserModels
{
    public class SignInModel
    {
        [Required, EmailAddress, MinLength(10)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
