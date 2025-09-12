namespace ExlaqiNasiri.Application.Features.Query
{
    public class GetSingleQueryResponse<T> where T : class
    {
        public int StatusCode { get; set; } = 200;
        public T Data { get; set; }
        public string Message { get; set; } = "";
    }
}
