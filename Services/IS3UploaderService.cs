using Microsoft.AspNetCore.Http;

namespace TFG_BACK.Services
{
    public interface IS3UploaderService
    {
        Task<string> SubirArchivoAsync(IFormFile archivo, string sufijo);
    }
}
