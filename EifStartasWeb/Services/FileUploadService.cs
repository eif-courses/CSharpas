using EifStartasWeb.Data;
using EifStartasWeb.Entities;
using EifStartasWeb.Services.Interfaces;

namespace EifStartasWeb.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _dbContext;

    public FileUploadService(IWebHostEnvironment env, ApplicationDbContext dbContext)
    {
        _env = env;
        _dbContext = dbContext;
    }

    public async Task<string> UploadFileAsync(IFormFile file, int studentRecordId)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file!");

        var allowedExtensions = new[] { ".zip", ".mp4", ".avi", ".mov", ".mkv" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            throw new InvalidOperationException("Only ZIP and video files are allowed!");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var document = new Document
        {
            DocumentType = fileExtension == ".zip" ? "Zip File" : "Video File",
            FilePath = filePath,
            UploadedDate = DateTime.UtcNow,
            StudentRecordId = studentRecordId
        };

        _dbContext.Documents.Add(document);
        await _dbContext.SaveChangesAsync();

        return filePath;
    }
}
