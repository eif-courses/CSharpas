using EifStartasWeb.Data;
using EifStartasWeb.Entities;
using EifStartasWeb.Services.Interfaces;
using OfficeOpenXml;

namespace EifStartasWeb.Services;

public class ImportFromExcelService : IDataImportService
{
    private readonly ApplicationDbContext _context;

    public ImportFromExcelService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ImportStudentRecordsFromExcel(string filePath)
    {
        try
        {
            Console.WriteLine($"Reading file: {filePath}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found!");
                return;
            }

            var studentRecords = new List<StudentRecord>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    Console.WriteLine("Error: Empty Excel file.");
                    return;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var studentRecord = new StudentRecord
                    {
                        Group = worksheet.Cells[row, 1].Text,
                        StudentName = worksheet.Cells[row, 2].Text,
                        SupervisorName = worksheet.Cells[row, 3].Text,
                        StudyProgram = worksheet.Cells[row, 4].Text,
                        CurrentYear = int.TryParse(worksheet.Cells[row, 5].Text, out var year) ? year : 0,
                        ReviewerEmail = worksheet.Cells[row, 6].Text,
                    };

                    studentRecords.Add(studentRecord);
                }
            }

            await _context.StudentRecords.AddRangeAsync(studentRecords);
            await _context.SaveChangesAsync();
            Console.WriteLine("Import successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during import: {ex.Message}");
        }
    }
}