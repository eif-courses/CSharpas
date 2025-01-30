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
    public string UserId { get; set; }
    public IdentityUser User { get; set; }  

    public DocumentPermissionType PermissionType { get; set; } 
}