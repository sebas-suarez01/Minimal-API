namespace Minimal_API.Domain.Errors;

public class ErrorCode
{
    public const string DuplicateId = "Model.DuplicateId";
    public const string DuplicateIndex = "Model.DuplicateIndex";
    public const string ModelNotFound = "Model.ModelNotFound";
    public const string IdNotFound = "Model.IdNotFound";
    public const string IdsNotMatch = "Model.IdsNotMatch";
    public const string NullValue = "Error.NullValue";
    public const string UsernameNotFound = "Auth.UsernameNotFound";
    public const string DuplicateUsername = "Auth.DuplicateUsername";    
    public const string DuplicateEmail = "Auth.DuplicateEmail";
    public const string InvalidCredentials = "Auth.InvalidCredentials";
    public const string PasswordAndConfirmNotMatch = "Auth.PasswordAndConfirmNotMatch";
    public const string Validation = "Model.Validation";
    public const string Internal = "Error.SomethingOcurred";
    public const string Transaction = "Error.TransactionFailed";
    public const string RoleNotFound = "RoleModel.RoleNotFound";
    public const string ItemNameNotFound = "ItemModel.NameNotFound";



}
