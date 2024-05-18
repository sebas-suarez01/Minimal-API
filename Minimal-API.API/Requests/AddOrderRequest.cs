namespace Minimal_API.API.Requests;

public record AddOrderRequest(Guid UserId, ItemRequest[] Items);