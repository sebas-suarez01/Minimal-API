using MediatR;
using Minimal_API.Application.Interfaces;

namespace Minimal_API.Application.Behaviors;

public class SaveChangesBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest:notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveChangesBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        var transaction = _unitOfWork.BeginTransaction();

        var response = await next();

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        transaction.Commit();

        return response;
    }
    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.Contains("Command");
    }
}