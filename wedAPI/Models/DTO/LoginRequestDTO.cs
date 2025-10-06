﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI_simple.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
