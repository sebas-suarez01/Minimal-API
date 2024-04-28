using Minimal_API.Application.Abstractions;

namespace Minimal_API.Application.Items.Commands.AddItem;

public record AddItemCommand(string Name, decimal Price) : ICommand<Guid>;