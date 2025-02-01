using EifStartasWeb.Services.Interfaces;

namespace EifStartasWeb.Services;

public class CloudflareR2Service : IStorageService
{
    public Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        throw new NotImplementedException();
    }
}