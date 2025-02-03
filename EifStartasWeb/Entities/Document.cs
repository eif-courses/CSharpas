using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Entities;
// https://app.mockflow.com/view/MkI1gJwDaqb




public class StudentRecord
{
    public int Id { get; set; } // Unique identifier for the record
    public string Group { get; set; } // Group (e.g., PI22S)
    public string StudentName { get; set; } // Student name (e.g., Poska Arminas)
    public string SupervisorName { get; set; } // Supervisor name (e.g., Gžegoževskis Marius)
    public string StudyProgram { get; set; } // Study program (e.g., Programų sistemos)
    public int CurrentYear { get; set; } // Current year (e.g., 2025)
    public string ReviewerEmail { get; set; } // Reviewer email (e.g., PS1@viko.lt)
  
    // Navigation property to the documents associated with the student
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    
    // Navigation property to the reports associated with the student
    public ICollection<Report> Reports { get; set; } = new List<Report>();
    
    
    
}public class Document
{
    public int Id { get; set; } // Unique identifier for the document
    public int StudentRecordId { get; set; } // Foreign key to StudentRecord
    public string DocumentType { get; set; } // Type of document (e.g., "Required Document 1")
    public string FilePath { get; set; } // Path or URL to the uploaded file
    public DateTime UploadedDate { get; set; } // Date when the document was uploaded

    // Navigation property to the associated StudentRecord
    public StudentRecord StudentRecord { get; set; }
}

public class Report
{
    public int Id { get; set; } // Unique identifier for the report
    public int StudentRecordId { get; set; } // Foreign key to StudentRecord
    public string Content { get; set; } // Content of the report
    public string Author { get; set; } // Author of the report (e.g., Supervisor or Reviewer)
    public DateTime CreatedDate { get; set; } // Date when the report was created

    // Navigation property to the associated StudentRecord
    public StudentRecord StudentRecord { get; set; }
}




public static class DocumentModelBuilderExtensions
{
    public static void ConfigureDocument(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentRecord>()
            .HasKey(sr => sr.Id); // Primary key for StudentRecord

        modelBuilder.Entity<Document>()
            .HasKey(d => d.Id); // Primary key for Document

        modelBuilder.Entity<Report>()
            .HasKey(r => r.Id); // Primary key for Report

        // Relationships
        modelBuilder.Entity<Document>()
            .HasOne(d => d.StudentRecord)
            .WithMany(sr => sr.Documents)
            .HasForeignKey(d => d.StudentRecordId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.StudentRecord)
            .WithMany(sr => sr.Reports)
            .HasForeignKey(r => r.StudentRecordId);
    }
}
