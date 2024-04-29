using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Violation: IDomainEntityId
{
    public Guid Id { get; set; }

}