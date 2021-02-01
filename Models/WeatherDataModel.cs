using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VäderApp.Models
{
    public class WeatherDataModel
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public double AverageTemperature { get; set; }
        public int AverageHumidity { get; set; }
        public int MoldRisk { get; set; }

    }
}
