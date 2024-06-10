using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket;

public sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public sealed record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}

public class DeleteBasketHandler(
    IBasketRepository basketRepository
) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}