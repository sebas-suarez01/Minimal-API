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

    public AuthRepository(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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
        
        var passwordhash = "";
        var user = UserModel.Create(registerModel.Username, registerModel.Name, registerModel.LastName,
            registerModel.Email, passwordhash, registerModel.PhoneNumber);

        var roleResult = await _roleRepository.GetRoleByNameAsync(RoleMapping.MapRole(roleValue));

        user.Role = roleResult.Value;
        
        var id = await _userRepository.CreateAsync(user);
        
        return id;
    }

    public Task Login(LoginModel loginModel)
    {
        throw new NotImplementedException();
    }
}