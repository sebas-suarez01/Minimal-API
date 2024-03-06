using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Common;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Roles.Shared;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtProvider _jwtProvider;

    public AuthRepository(IUserRepository userRepository, IRoleRepository roleRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<Guid>> Register(RegisterModel registerModel, RoleEnum roleValue = RoleEnum.User)
    {
        if (registerModel.Password != registerModel.ConfirmPassword)
        {
            return Result.Failure<Guid>(
                ErrorTypes.Models.PasswordAndConfirmNotMatch(registerModel.Password, registerModel.ConfirmPassword));
        }
        
        var userDtoResult = await _userRepository.GetByUsernameAsync(registerModel.Username);

        if (userDtoResult.IsFailure)
        {
            return Result.Failure<Guid>(userDtoResult.Errors[0]);
        }
        
        userDtoResult = await _userRepository.GetByEmailAsync(registerModel.Email);
        
        if (userDtoResult.IsFailure)
        {
            return Result.Failure<Guid>(userDtoResult.Errors[0]);
        }
        
        var passwordhash = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);
        
        var user = UserModel.Create(registerModel.Username, registerModel.Name, registerModel.LastName,
            registerModel.Email, passwordhash, registerModel.PhoneNumber);

        var roleResult = await _roleRepository.GetRoleByNameAsync(RoleMapping.MapRole(roleValue));

        user.Role = roleResult.Value;
        
        var id = await _userRepository.CreateAsync(user);
        
        return id;
    }

    public async Task<Result<string>> Login(LoginModel loginModel)
    {
        var userDtoResult = await _userRepository.GetByUsernameAsync(loginModel.Username);
        
        if (userDtoResult.IsFailure)
        {
            return Result.Failure<string>(ErrorTypes.Models.InvalidCredentials());
        }

        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, userDtoResult.Value.PasswordHash);

        if (!isCorrectPassword)
        {
            return Result.Failure<string>(ErrorTypes.Models.InvalidCredentials());
        }

        var token = _jwtProvider.Generate(userDtoResult.Value);

        return token;
    }
}