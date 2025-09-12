using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.Abstraction.Helper
{
    public interface IFileUpload
    {
        public string UploadFile(IFormFile file, string root, string path);
    }
}
