using MediatR;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Abstractions
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
