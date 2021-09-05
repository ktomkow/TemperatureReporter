using System;

namespace TemperatureReporter.App
{
    public class FakeMeasurer : IMeasurer
    {
        private readonly Random random;

        public FakeMeasurer()
        {
            this.random = new Random();
        }

        public Measurement Get()
        {
            int randInt = this.random.Next(40, 80);

            return new Measurement(randInt + this.random.NextDouble());
        }
    }
}
