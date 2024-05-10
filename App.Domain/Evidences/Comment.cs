using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using App.Domain.Identity;
using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Evidences;

public class Comment : BaseEntityId
{

    public string? CommentText { get; set; }
    public Guid? ParentCommentId { get; set; }
    
    public Comment? ParentComment { get; set; }
    public Guid AccountId { get; set; }
    public AppUser? Account { get; set; }
    
    public Guid VehicleViolationId { get; set; }
    public VehicleViolation? VehicleViolation { get; set; }
    
    public ICollection<Comment>? ChildComments { get; set; } = new List<Comment>();

    private DateTime CreatedAt { get; set; } = DateTime.Now;

}
