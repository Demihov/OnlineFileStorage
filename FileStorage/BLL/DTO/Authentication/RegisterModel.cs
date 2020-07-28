using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.Authentication
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
