using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Entities;
// https://app.mockflow.com/view/MkI1gJwDaqb
public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Url { get; set; }
    public string Type { get; set; } 
    public string Size { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; } 
    public DateTime? DeletedAt { get; set; }

    public string UserId { get; set; } 
    public IdentityUser User { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; } // Foreign key to IdentityUser
    public IdentityUser User { get; set; }

    public ICollection<Document> Documents { get; set; }
    public int SupervisorId { get; set; }
    public Supervisor Supervisor { get; set; }
}

public class Supervisor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; } // Foreign key to IdentityUser
    public IdentityUser User { get; set; }

    public ICollection<Student> Students { get; set; }
}

public class ExternalReviewer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; } // Foreign key to IdentityUser
    public IdentityUser User { get; set; }

    public ICollection<ExternalReviewerReport> Reviews { get; set; }
}


public enum ReviewStatus
{
    Draft,
    Submitted,
    Approved,
    Rejected
}


public class SupervisorReport
{
    public int Id { get; set; }
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int DocumentId { get; set; }
    public Document Document { get; set; }

    public string SupervisorId { get; set; }
    public IdentityUser Supervisor { get; set; }
    
    public ReviewStatus Status { get; set; }
}

public class ExternalReviewerReport
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int DocumentId { get; set; }
    public Document Document { get; set; }

    public int ReviewerId { get; set; }
    public ExternalReviewer Reviewer { get; set; }
    
    public ReviewStatus Status { get; set; }
    
}




public static class DocumentModelBuilderExtensions
{
    public static void ConfigureDocument(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Title).IsRequired().HasMaxLength(255);
            entity.Property(d => d.Url).IsRequired();
            entity.Property(d => d.Type).IsRequired().HasMaxLength(50);
            entity.Property(d => d.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(d => d.UpdatedAt).IsConcurrencyToken();
            entity.Property(d => d.DeletedAt);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Student)
                .WithMany(s => s.Documents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);

            entity.HasOne(s => s.Supervisor)
                .WithMany(su => su.Students)
                .HasForeignKey(s => s.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(su => su.Id);
            entity.Property(su => su.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<ExternalReviewer>(entity =>
        {
            entity.HasKey(er => er.Id);
            entity.Property(er => er.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<SupervisorReport>(entity =>
        {
            entity.HasKey(sr => sr.Id);
            entity.Property(sr => sr.Content).IsRequired();

            entity.HasOne(sr => sr.Document)
                .WithMany()
                .HasForeignKey(sr => sr.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sr => sr.Supervisor)
                .WithMany()
                .HasForeignKey(sr => sr.SupervisorId) // Ensure this is correctly mapped
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExternalReviewerReport>(entity =>
        {
            entity.HasKey(err => err.Id);
            entity.Property(err => err.Content).IsRequired();
            
            entity.HasOne(err => err.Document)
                .WithMany()
                .HasForeignKey(err => err.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(err => err.Reviewer)
                .WithMany(er => er.Reviews)
                .HasForeignKey(err => err.ReviewerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
