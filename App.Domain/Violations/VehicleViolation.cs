﻿using App.Domain.Evidences;
using App.Domain.Identity;
using App.Domain.Vehicles;

using Base.Domain;

namespace App.Domain.Violations;

public class VehicleViolation : BaseEntityId
{
    public Guid VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public Guid ViolationId { get; set; }
    public Violation? Violation { get; set; }

    public Guid AccountId { get; set; }
    public AppUser? Account { get; set; }
    public string? Description { get; set; }
    
    public string? Coordinates { get; set; }
    
    public string? LocationName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<AdditionalVehicle>? AdditionalVehicles { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Evidence>? Evidences { get; set; }
    

}
