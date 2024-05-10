namespace App.DTO.v1_0;


public class Comment 
{
    public Guid Id { get; set; }
    
    public string? CommentText { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Guid VehicleViolationId { get; set; }

    private DateTime CreatedAt { get; set; } 

}
