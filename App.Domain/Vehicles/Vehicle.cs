﻿using App.Domain.Violations;
using Base.Domain;

namespace App.Domain.Vehicles;

public class Vehicle : BaseEntityId
{
    public Guid VehicleTypeId { get; set; }
    public VehicleType? VehicleType { get; set; }

    public string Color { get; set; } = default!;
    
    public string? RegNr { get; set; }
    
    public decimal? Rating { get; set; }
    
    public ICollection<VehicleViolation>? VehicleViolations { get; set; }
    public ICollection<AdditionalVehicle>? AdditionalVehicles { get; set; }
    
}
