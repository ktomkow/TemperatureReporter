﻿using Iot.Device.CpuTemperature;
using System;
using System.Threading;

namespace TemperatureReporter.App
{
    class Measurer : IMeasurer
    {
        private CpuTemperature measurer;

        public Measurer()
        {
            this.measurer = new CpuTemperature();
        }

        public Measurement Get()
        {
            double temperature = measurer.Temperature.DegreesCelsius;

            return new Measurement(temperature);
        }
    }
}
