﻿namespace App.DTO.v1_0;

public class ViolationType
{
    public Guid Id { get; set; }
    
    public string? ViolationTypeName{ get; set; }
    
    public decimal? Severity { get; set; }
}