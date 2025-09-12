using System.ComponentModel.DataAnnotations;

namespace ExlaqiNasiri.Application.DTOs.Authentication
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
