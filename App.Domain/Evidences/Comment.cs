using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Evidences;

public class Comment : BaseEntityId
{
    [MaxLength(256)] 
    public string? CommentText { get; set; }
    public Guid? ParentCommentId { get; set; }
    
    public Comment? ParentComment { get; set; }
    public Guid AccountId { get; set; }
    public AppUser? Account { get; set; }
    
    public Guid VehicleViolationId { get; set; }
    public VehicleViolation? VehicleViolation { get; set; }
    
    public ICollection<Comment>? ChildComments { get; set; } = new List<Comment>();
    
    private DateTime _createdAt;

    public DateTime CreatedAt
    {
        get { return _createdAt; }
        set { _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
    
}
