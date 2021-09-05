using System;

namespace TemperatureReporter.App
{
    public class Measurement
    {
        public double Temperature { get; }
        public DateTime At { get; }

        protected Measurement()
        {

        }

        public Measurement(double temperature)
        {
            Temperature = temperature;
            At = DateTime.UtcNow;
        }
    }
}
