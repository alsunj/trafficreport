namespace Base.Contracts.Domain;

public interface IDomainEntityMetaData
{
    public string createdBy { get; set; }
    public DateTime createdAt { get; set; }
    public string updatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}