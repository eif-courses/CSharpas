using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Data;

public static class SeedData
{
    private const string AdminRoleId = "01H6N7NV1KTPB9QDZ7FYDJ3HHK";
    private const string ReviewerRoleId = "01H6N7NV1JHYY7N2NFDYX4ATAP";
    private const string SupervisorRoleId = "01H6N7NV1YTMCV8YPZC7QQGGG7";
    private const string StudentRoleId = "01H6N7NV18JWC8MYPXCVZR9WZW";
    private const string DepartmentRoleId = "01H6N7NV1MHQDXGNYH2HQT34V9";
    
    public static void Initialize(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        SeedUsers(modelBuilder);
    }
    
    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = AdminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = ReviewerRoleId, Name = "Reviewer", NormalizedName = "REVIEWER" },
            new IdentityRole { Id = StudentRoleId, Name = "Student", NormalizedName = "STUDENT" },
            new IdentityRole { Id = SupervisorRoleId, Name = "Supervisor", NormalizedName = "SUPERVISOR" },
            new IdentityRole { Id = DepartmentRoleId, Name = "Department", NormalizedName = "DEPARTMENT" }
        );
    }
    
    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<IdentityUser>();
        var adminUser = new IdentityUser
        {
            Id = "01H6N7NV2P1KCVKY7F6EJH0FAF",
            UserName = "admin@viko.lt",
            NormalizedUserName = "ADMIN@VIKO.LT",
            Email = "admin@viko.lt",
            NormalizedEmail = "ADMIN@VIKO.LT",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Kolegija1@");
        modelBuilder.Entity<IdentityUser>().HasData(adminUser);
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            UserId = "01H6N7NV2P1KCVKY7F6EJH0FAF",
            RoleId = AdminRoleId
        });
    }
    
    

}