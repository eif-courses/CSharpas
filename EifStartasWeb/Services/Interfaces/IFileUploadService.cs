namespace EifStartasWeb.Services.Interfaces;

public interface IFileUploadService
{
    Task<string> UploadFileAsync(IFormFile file, int studentRecordId);
}