using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Comment : IDomainEntityId
{
    public Guid Id { get; set; }

    public string? CommentText { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Guid VehicleViolationId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
