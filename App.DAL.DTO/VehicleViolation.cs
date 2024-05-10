﻿using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class VehicleViolation: IDomainEntityId 
{
    public Guid Id { get; set; }
    
    public Guid VehicleId { get; set; }

    public Guid ViolationId { get; set; }

    public Guid AccountId { get; set; }
    
    public string? Description { get; set; }
    
    public string? Coordinates { get; set; }
    
    public string? LocationName { get; set; }

    public DateTime CreatedAt { get; set; }
}