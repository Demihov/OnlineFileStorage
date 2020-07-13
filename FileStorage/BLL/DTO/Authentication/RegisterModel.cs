using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO.Authentication
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        public string UserName { get; set; }

        public int Year { get; set; }
    }
}
