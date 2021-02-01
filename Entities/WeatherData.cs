﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VäderApp.Entities
{
    public class WeatherData
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
    }
}
