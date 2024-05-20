﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}
