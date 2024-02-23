using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class AdditionalVehicle : BaseEntityId
{
    [MaxLength(128)] 
    public string  { get; set; }
    
    [MaxLength(128)] 
    public string Username { get; set; }
    
    [MaxLength(128)] 
    public string Username { get; set; }
}