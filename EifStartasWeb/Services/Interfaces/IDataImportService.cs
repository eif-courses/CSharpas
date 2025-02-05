namespace EifStartasWeb.Services.Interfaces;

public interface IDataImportService
{
    public Task ImportStudentRecordsFromExcel(string filePath);
}