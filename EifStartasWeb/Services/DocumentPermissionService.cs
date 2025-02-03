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