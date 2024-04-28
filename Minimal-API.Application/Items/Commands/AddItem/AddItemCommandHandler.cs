using MediatR;
using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.DomainEvents;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Items.Commands.AddItem;

public class AddItemCommandHandler : ICommandHandler<AddItemCommand, Guid>
{
    private readonly IItemRepository _repository;
    private readonly IPublisher _publisher;

    public AddItemCommandHandler(IItemRepository repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = ItemModel.Create(request.Name, request.Price);
        var itemIdResult = await _repository.CreateAsync(item, cancellationToken);
        
        return itemIdResult;
    }
}