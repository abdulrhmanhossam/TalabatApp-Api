using System.Text.Json;
using StackExchange.Redis;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repository;
public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task<bool> DeleteCustomerBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }

    public async Task<CustomerBasket> GetCustomerBasketAsync(string basketId)
    {
        var basket = await _database.StringGetAsync(basketId);
        return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
    }

    public async Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket basket)
    {
        var createOrUpdate = await _database
            .StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        
        if (!createOrUpdate)
            return null;

        return await GetCustomerBasketAsync(basket.Id);
    }
}