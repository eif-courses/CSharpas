namespace EifStartasWeb.Services;

public interface IDataImportService
{
    public Task ImportFromExcelAsync(string filePath);
}