using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Threading.Tasks;

namespace TemperatureReporter.App.Store
{
    public class RedisCache
    {
        private IRedisCacheClient redis;

        private IRedisDatabase redisDb => redis.GetDbFromConfiguration();

        private readonly string key = nameof(Measurement);

        public RedisCache(IRedisCacheClient redis)
        {
            this.redis = redis;
        }

        public async Task<Measurement> Get(string key)
        {
            Measurement obj = await this.redisDb.GetAsync<Measurement>(key);

            return obj;
        }

        public async Task Set(Measurement obj)
        {
            await this.redisDb.AddAsync(key, obj);
        }
    }
}
