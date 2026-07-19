using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace WebApplication1
{
    //public class RedisCacheService : IRedisCacheService
    //{
    //    private readonly IDatabase _database;

    //    public RedisCacheService(IConnectionMultiplexer redis)
    //    {
    //        _database = redis.GetDatabase();
    //    }

    //    public async Task<T?> GetAsync<T>(string key)
    //    {
    //        var value = await _database.StringGetAsync(key);

    //        if (value.IsNullOrEmpty)
    //            return default;

    //        return null;
    //        //return JsonSerializer.Deserialize<T>(value!);
    //    }

    //    public async Task SetAsync<T>(string key, T value, TimeSpan expiry)
    //    {
    //        var json = JsonSerializer.Serialize(value);

    //        await _database.StringSetAsync(key, json, expiry);
    //    }

    //    public async Task RemoveAsync(string key)
    //    {
    //        await _database.KeyDeleteAsync(key);
    //    }
    //}
}
