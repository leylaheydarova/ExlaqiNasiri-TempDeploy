using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExlaqiNasiri.Application.DTOs.WebUser
{
    public class WebUserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText(true)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [PasswordPropertyText(true)]
        public string ConfirmPassword { get; set; }
    }
}
