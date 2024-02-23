using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Account : BaseEntityId
{
    [MaxLength(128)] 
    public string Username { get; set; }
    
}