using EifStartasWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentPermission> DocumentPermissions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<ExternalReviewer> ExternalReviewers { get; set; }
    
    public DbSet<ExternalReviewerReport> ExternalReviewerReports { get; set; }
    public DbSet<SupervisorReport> SupervisorReports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);
        builder.ConfigureDocument(); // inicijuojame sąryšius tarp entity
    }
}