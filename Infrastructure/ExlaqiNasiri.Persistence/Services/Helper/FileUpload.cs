using ExlaqiNasiri.Application.Abstraction.Helper;
using Microsoft.AspNetCore.Http;
namespace ExlaqiNasiri.Persistence.Services.Helper
{
    public class FileUpload : IFileUpload
    {
        public string UploadFile(IFormFile file, string root, string path)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string fullpath = Path.Combine(root, path, filename);

            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
    }
}
