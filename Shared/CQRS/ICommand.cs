using MediatR;

namespace Shared.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse> { }
public interface ICommand : ICommand<Unit> { }

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> { };
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand { };
