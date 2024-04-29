using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class VehicleViolation: IDomainEntityId
{
    public Guid Id { get; set; }

}