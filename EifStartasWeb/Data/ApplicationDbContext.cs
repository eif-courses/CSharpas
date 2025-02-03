using EifStartasWeb.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<StudentRecord> StudentRecords { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Report> Reports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);
        builder.ConfigureDocument(); // inicijuojame sąryšius tarp entity
    }
}