using EifStartasWeb.Data;
using EifStartasWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace EifStartasWeb.Services;

public class DocumentPermissionService
{
    private readonly ApplicationDbContext _dbContext;

    public DocumentPermissionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task GrantDefaultPermissionsAsync(Document document, string studentId)
    {
        var student = await _dbContext.Students
            .Include(s => s.Supervisor)
            .FirstOrDefaultAsync(s => s.UserId == studentId);

        if (student == null || student.Supervisor == null)
            return;

        // Grant Supervisor access
        _dbContext.DocumentPermissions.Add(new DocumentPermission
        {
            DocumentId = document.Id,
            SupervisorId = student.Supervisor.Id,
            PermissionType = DocumentPermissionType.View
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CanUserAccessDocumentAsync(string userId, int documentId)
    {
        return await _dbContext.DocumentPermissions
            .AnyAsync(dp =>
                dp.DocumentId == documentId &&
                (dp.UserId == userId ||
                 dp.Supervisor.UserId == userId ||
                 dp.Reviewer.UserId == userId));
    }
}