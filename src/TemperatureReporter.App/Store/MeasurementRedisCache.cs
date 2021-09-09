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

        private const string Key = nameof(CpuMeasurement);

        public MeasurementRedisCache(IRedisCacheClient redis)
        {
            this.redis = redis;
        }

        public async Task<CpuMeasurement> Get()
        {
            MeasurementCacheModel cacheModel = await this.redisDb.GetAsync<MeasurementCacheModel>(Key);

            if(cacheModel is null)
            {
                return null;
            }

            CpuMeasurement result = cacheModel.Map();

            return result;
        }

        public async Task Set(CpuMeasurement input)
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

            public CpuMeasurement Map()
            {
                CpuMeasurement measurement = new CpuMeasurement(this.Temperature);

                FieldInfo field = typeof(CpuMeasurement).GetField("<At>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(measurement, this.At);

                return measurement;
            }

            public static MeasurementCacheModel Map(CpuMeasurement measurement)
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
