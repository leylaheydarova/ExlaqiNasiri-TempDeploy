using ExlaqiNasiri.Application.Enums;
using System.Net;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IError
    {
        public string Title { get; init; }
        public ErrorType Type { get; init; }
        public string Message { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public IEnumerable<KeyValuePair<string, string[]>> ValidationErrorMessages { get; init; }
    }
}
