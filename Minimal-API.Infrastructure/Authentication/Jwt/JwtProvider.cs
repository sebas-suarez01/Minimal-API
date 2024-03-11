using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Users;

namespace Minimal_API.Infrastructure.Authentication.Jwt;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IUserRepository _userRepository;

    public JwtProvider(IOptions<JwtOptions> options, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _options = options.Value;
    }

    public async Task<string> Generate(UserDto user)
    {
        var expirationDate = DateTime.Now.AddHours(1);
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _options.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _options.Audience),
            new Claim(ClaimTypes.Role, user.Role.Name)

        };
        var permissions = await _userRepository.GetPermissionsAsync(user.Id);

        foreach (var permission in permissions.Value)   
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            expirationDate,
            signingCredentials);
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}