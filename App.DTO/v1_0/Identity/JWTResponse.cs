﻿namespace App.DTO.v1_0.Identity;

public class JWTResponse
{
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    
    public string? Id { get; set; } // For testing only
}