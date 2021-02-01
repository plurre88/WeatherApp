using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VäderApp.Data;
using VäderApp.Models;
using VäderApp.Entities;

namespace VäderApp.Services
{
    public class FetchDataService : IFetchDataService
    {
        private readonly EFContext _EFContext;
        public FetchDataService(EFContext eFContext)
        {
            _EFContext = eFContext;
        }
        
        public double GetDateAvgTemp(string e, string l)
        {
            var date = DateTime.Parse(e);
            var allDataOneDate = _EFContext.WeatherDatas
                .Where(d => d.Date.Year == date.Year && d.Date.Month == date.Month && d.Date.Day == date.Day && d.Location == l)
                .Select(d => d.Temperature)
                .ToList();

            return Math.Round(allDataOneDate.Sum() / allDataOneDate.Count(),2);
        }

        public List<WeatherDataModel> FetchDataToModel()
        {
            List<WeatherDataModel> dataModels = new List<WeatherDataModel>();

            var dates = _EFContext.WeatherDatas // Tar ut vilka olika datum som finns i listan
                .AsEnumerable()
                .GroupBy(d => d.Date.Date)
                .ToList();

            foreach(var date in dates) // gör en lista med all data för varje datum (varje datum får en enskild lista)
            {
                var dateAvg = _EFContext.WeatherDatas
                    .Where(d => d.Date.Date == date.Key);
                CalculateAndAddToList(dateAvg.Where(t => t.Location == "Inne"), dataModels, "Inne", date.Key);
                CalculateAndAddToList(dateAvg.Where(t => t.Location == "Ute"), dataModels, "Ute", date.Key);
            }
                
            return dataModels;
        }
        public void CalculateAndAddToList(IQueryable<WeatherData> listToCalc, List<WeatherDataModel> listToAdd, string location, DateTime date)
        {
            double moldRisk, averageTemperature = 0, averageHumidity = 0;

            foreach (var tempreading in listToCalc)
            {
                averageTemperature += tempreading.Temperature;
                averageHumidity += tempreading.Humidity;                
            }
            averageTemperature /= listToCalc.Count();
            averageHumidity /= listToCalc.Count();
            
            if ((((averageHumidity - 78) * (averageTemperature / 15)) / 0.22) < 0)
            {
                moldRisk = 0;
            }
            else if ((((averageHumidity - 78) * (averageTemperature / 15)) / 0.22) > 100)
            {
                moldRisk = 100;
            }
            else
            {
                moldRisk = ((averageHumidity - 78) * (averageTemperature / 15)) / 0.22;
            }

            WeatherDataModel wd = new WeatherDataModel()
            {
                AverageHumidity = (int)averageHumidity,
                AverageTemperature = Math.Round(averageTemperature,2),
                Date = date,
                Location = location,
                MoldRisk = (int)moldRisk
            };
            listToAdd.Add(wd);
        }
    }
}
