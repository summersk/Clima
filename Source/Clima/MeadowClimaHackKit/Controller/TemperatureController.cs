﻿using Meadow.Foundation.Sensors.Temperature;
using Meadow.Units;
using MeadowClimaHackKit.Database;
using System;

namespace MeadowClimaHackKit.Controller
{
    public class TemperatureController
    {
        private static readonly Lazy<TemperatureController> instance =
            new Lazy<TemperatureController>(() => new TemperatureController());
        public static TemperatureController Instance => instance.Value;

        public event EventHandler<Temperature> TemperatureUpdated = delegate { };

        AnalogTemperature analogTemperature;

        private TemperatureController() { }

        public void Initialize()
        {
            analogTemperature = new AnalogTemperature(MeadowApp.Device,
                MeadowApp.Device.Pins.A01, AnalogTemperature.KnownSensorType.LM35);
            analogTemperature.StartUpdating(TimeSpan.FromSeconds(30));
            analogTemperature.TemperatureUpdated += AnalogTemperatureUpdated;
        }

        void AnalogTemperatureUpdated(object sender, Meadow.IChangeResult<Temperature> e)
        {
            Console.Write($"Saving ({e.New.Celsius},{DateTime.Now})...");

            var reading = new TemperatureTable()
            {
                TemperatureValue = e.New,
                DateTime = DateTime.Now
            };
            DatabaseManager.Instance.SaveReading(reading);

            TemperatureUpdated.Invoke(this, e.New);

            DisplayController.Instance.UpdateDisplay(e.New);

            Console.WriteLine("done!");
        }
    }
}