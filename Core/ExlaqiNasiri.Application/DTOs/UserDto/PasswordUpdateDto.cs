using System.ComponentModel.DataAnnotations;

namespace ExlaqiNasiri.Application.DTOs.UserDto
{
    public class PasswordUpdateDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
