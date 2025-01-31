using Microsoft.AspNetCore.Identity;

namespace EifStartasWeb.Entities;

public enum DocumentPermissionType
{
    View,
    Edit,
    Delete
}


public class DocumentPermission
{
    public int Id { get; set; }

    public int DocumentId { get; set; }
    public Document Document { get; set; }

    public string? UserId { get; set; }  // For individual access
    public IdentityUser? User { get; set; } 

    public int? SupervisorId { get; set; }  // For supervisor-based access
    public Supervisor? Supervisor { get; set; }

    public int? ReviewerId { get; set; }  // For external reviewer access
    public ExternalReviewer? Reviewer { get; set; }

    public DocumentPermissionType PermissionType { get; set; }
}