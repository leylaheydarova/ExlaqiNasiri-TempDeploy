namespace ExlaqiNasiri.Application.Utils
{
    public static class GenerateSequence
    {
        public static string GenerateOtpCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
