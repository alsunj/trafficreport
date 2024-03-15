using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class BaseEntityIdMetaData : BaseEntityMetaData<Guid>, IDomainEntityMetaData
{
    
}

public abstract class BaseEntityMetaData<TKey> : BaseEntityId<TKey>, IDomainEntityMetaData
    where TKey : IEquatable<TKey>
{
    public string createdBy { get; set; }
    public DateTime createdAt { get; set; }
    public string updatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}