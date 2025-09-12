namespace ExlaqiNasiri.Application.Features.Command
{
    public class CreateCommandRequest<T> where T : class
    {
        public T Dto { get; set; }
    }
}
