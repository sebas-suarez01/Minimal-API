using System.Net;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Domain.Errors;

public static partial class ErrorTypes
{
    public static class Models 
    {
        public static Error IdNotFound<T>(T id)
            where T : notnull
            => new(
            HttpStatusCode.NotFound,
            ErrorCode.IdNotFound,
            $"Id: {id.ToString()} does not exist");
        
        public static Error RoleNotFound(string name)
            => new(
                HttpStatusCode.NotFound,
                ErrorCode.RoleNotFound,
                $"Role: {name} does not exist");
        public static Error UserNotFound(string username)
            => new(
                HttpStatusCode.NotFound,
                ErrorCode.UsernameNotFound,
                $"Username: {username} does not exist");
        
        public static Error PasswordAndConfirmNotMatch(string password, string confirmPassword)
            => new(
                HttpStatusCode.Conflict,
                ErrorCode.PasswordAndConfirmNotMatch,
                $"Password: {password} Confirm Password : {confirmPassword} do not match");
        public static Error InvalidCredentials()
            => new(
                HttpStatusCode.Unauthorized,
                ErrorCode.InvalidCredentials,
                $"Invalid Credentials");
    }
}