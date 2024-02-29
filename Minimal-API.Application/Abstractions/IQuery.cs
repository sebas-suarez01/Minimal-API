using MediatR;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Abstractions
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
