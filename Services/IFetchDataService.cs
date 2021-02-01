using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using VäderApp.Entities;
using VäderApp.Models;

namespace VäderApp.Services
{
    public interface IFetchDataService
    {
        double GetDateAvgTemp(string e, string l);
        List<WeatherDataModel> FetchDataToModel();
        void CalculateAndAddToList(IQueryable<WeatherData> listToCalc, List<WeatherDataModel> listToAdd, string location, DateTime date);
    }
}