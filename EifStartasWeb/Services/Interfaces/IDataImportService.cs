namespace EifStartasWeb.Services.Interfaces;

public interface IDataImportService
{
    public Task ImportFromExcelAsync(string filePath);
}