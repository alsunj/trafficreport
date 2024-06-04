using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using App.Domain.Identity;
using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Evidences;

public class Comment : BaseEntityId
{
    [MaxLength(256)]
    public string CommentText { get; set; } = default!;
    public Guid? ParentCommentId { get; set; }
    
    public Comment? ParentComment { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public Guid VehicleViolationId { get; set; }
    public VehicleViolation? VehicleViolation { get; set; }
    
    public ICollection<Comment>? ChildComments { get; set; } = new List<Comment>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
