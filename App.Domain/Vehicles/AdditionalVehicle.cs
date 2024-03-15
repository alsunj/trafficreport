using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Vehicles;

public class AdditionalVehicle : BaseEntityId
{
    [MaxLength(128)] 
    public string AdditionalVehicles { get; set; }
    
    [MaxLength(128)] 
    public string Username { get; set; }
    

}