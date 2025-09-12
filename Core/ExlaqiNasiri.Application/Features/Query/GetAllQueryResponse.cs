namespace ExlaqiNasiri.Application.Features.Query
{
    public class GetAllQueryResponse<T> where T : class
    {
        public int StatusCode { get; set; } = 200;
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public string Message { get; set; } = "";
    }
}
