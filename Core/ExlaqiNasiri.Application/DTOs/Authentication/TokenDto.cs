namespace ExlaqiNasiri.Application.DTOs.Authentication
{
    public class TokenDto
    {
        public DateTime ExpireDate { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
