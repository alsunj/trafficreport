namespace App.DTO.v1_0;


public class Comment 
{
    public Guid Id { get; set; }
    
    public string? CommentText { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public Guid AppUserId { get; set; }
    
    public Guid VehicleViolationId { get; set; }

    public DateTime CreatedAt { get; set; } 

}
