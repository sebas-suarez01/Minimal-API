﻿namespace Minimal_API.Infrastructure.Authentication.Jwt;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    
    
}