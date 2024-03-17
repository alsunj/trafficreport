using System.ComponentModel.DataAnnotations;
using App.Domain.Evidences;
using Base.Domain;

namespace App.Domain.Vehicles;

public class VehicleType : BaseEntityId
{
    [MaxLength(128)]
    public string? VehicleTypeName { get; set; }
    
    public EVehicleSize Size { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
    
}
