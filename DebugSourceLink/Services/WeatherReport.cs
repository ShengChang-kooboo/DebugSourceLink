using System;
using System.Collections.Generic;

namespace DebugSourceLink.Services
{
    public class WeatherReport
    {
        public class WeatherInfo
        {
            #region Members.
            public string Condition { get; set; }
            public double MaximumTemperature { get; set; }
            public double MinimumTemperature { get; set; }
            #endregion
        }

        #region Members.
        private static string[] _weatherConditions = new string[] {"霾", "小雨", "多云", "晴" };
        private static Random _random = new Random();

        public string City { get; }
        public Dictionary<DateTime, WeatherInfo> WeatherInfos { get; }
        #endregion

        #region Constructors.
        public WeatherReport(string city, int days)
        {
            City = city;
            WeatherInfos = new Dictionary<DateTime, WeatherInfo>();
            for (int i = 0; i < days; i++)
            {
                WeatherInfos[DateTime.Today.AddDays(i + 1)] = new WeatherInfo
                {
                    Condition = _weatherConditions[_random.Next(0, 4)],
                    MaximumTemperature = _random.Next(60, 70),
                    MinimumTemperature = _random.Next(50, 60)
                };
            }
        }

        public WeatherReport(string city, DateTime dateTime)
        {
            this.City = city;
            this.WeatherInfos = new Dictionary<DateTime, WeatherInfo>
            {
                [dateTime]=new WeatherInfo
                {
                    Condition = _weatherConditions[_random.Next(0, 4)],
                    MaximumTemperature = _random.Next(60, 70),
                    MinimumTemperature = _random.Next(50, 60)
                }
            };
        }
        #endregion

        #region Methods.

        #endregion
    }
}
