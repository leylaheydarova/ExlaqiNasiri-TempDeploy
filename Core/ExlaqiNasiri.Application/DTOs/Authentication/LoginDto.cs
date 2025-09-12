using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExlaqiNasiri.Application.DTOs.Authentication
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
