namespace EifStartasWeb.Services.Interfaces;

public interface IStorageService
{
    public Task<string> UploadFileAsync(Stream fileStream, string fileName);
}