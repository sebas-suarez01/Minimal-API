using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Items.Commands.AddItem;

public class AddItemCommandHandler : ICommandHandler<AddItemCommand, Guid>
{
    private readonly IItemRepository _repository;

    public AddItemCommandHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = ItemModel.Create(request.Name, request.Price);
        var itemId = await _repository.CreateAsync(item, cancellationToken);

        return itemId;
    }
}