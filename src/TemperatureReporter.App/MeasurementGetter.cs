using System.Threading.Tasks;
using TemperatureReporter.App.Store;

namespace TemperatureReporter.App
{
    public class MeasurementGetter
    {
        private readonly IMeasurer measurer;
        private readonly MeasurementRedisCache cache;

        public MeasurementGetter(IMeasurer measurer, MeasurementRedisCache cache)
        {
            this.measurer = measurer;
            this.cache = cache;
        }

        public async Task<Measurement> Get()
        {
            Measurement measurement = await this.cache.Get();

            if(measurement is null)
            {
                measurement = this.measurer.Get();
                await this.cache.Set(measurement);
            }

            return measurement;
        }
    }
}
