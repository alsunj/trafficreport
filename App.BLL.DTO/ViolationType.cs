using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class ViolationType: IDomainEntityId
{
    public Guid Id { get; set; }

}