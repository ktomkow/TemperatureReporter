using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemperatureReporter.App.Store;

namespace TemperatureReporter.App
{
    public class MeasurerRecorder : BackgroundService
    {
        private readonly IMeasurer measurer;
        private readonly MeasurementRedisCache cache;

        public MeasurerRecorder(IMeasurer measurer, MeasurementRedisCache cache)
        {
            this.measurer = measurer;
            this.cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(2000);

            while (true)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    CpuMeasurement measurement = this.measurer.Get();
                    await this.cache.Set(measurement);

                    await Task.Delay(5000);
                }
            }
        }
    }
}
