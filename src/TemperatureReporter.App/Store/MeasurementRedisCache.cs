using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TemperatureReporter.App.Store
{
    public class MeasurementRedisCache
    {
        private IRedisCacheClient redis;

        private IRedisDatabase redisDb => redis.GetDbFromConfiguration();

        private const string Key = nameof(Measurement);

        public MeasurementRedisCache(IRedisCacheClient redis)
        {
            this.redis = redis;
        }

        public async Task<Measurement> Get()
        {
            MeasurementCacheModel cacheModel = await this.redisDb.GetAsync<MeasurementCacheModel>(Key);

            if(cacheModel is null)
            {
                return null;
            }

            Measurement result = cacheModel.Map();

            return result;
        }

        public async Task Set(Measurement input)
        {
            if(await this.redisDb.ExistsAsync(Key))
            {
                await this.redisDb.RemoveAsync(Key);
            }

            MeasurementCacheModel cacheModel = MeasurementCacheModel.Map(input);
            await this.redisDb.AddAsync(Key, cacheModel);
        }

        private class MeasurementCacheModel
        {
            public double Temperature { get; set; }
            public DateTime At { get; set; }

            public Measurement Map()
            {
                Measurement measurement = new Measurement(this.Temperature);

                FieldInfo field = typeof(Measurement).GetField("<At>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(measurement, this.At);

                return measurement;
            }

            public static MeasurementCacheModel Map(Measurement measurement)
            {
                MeasurementCacheModel cacheModel = new MeasurementCacheModel()
                {
                    Temperature = measurement.Temperature,
                    At = measurement.At
                };

                return cacheModel;
            }
        }
    }
}
