
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(
    IDistributedCache cache,
    IBasketRepository basketRepository) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(userName, cancellationToken);
        return await basketRepository.DeleteBasket(userName, cancellationToken);
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!cachedBasket.IsEmpty())
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket!)!;

        var basket = await basketRepository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return await basketRepository.StoreBasket(basket, cancellationToken);
    }
}