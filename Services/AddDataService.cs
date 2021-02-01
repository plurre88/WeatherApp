using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VäderApp.Entities;
using VäderApp.Data;
using System.Globalization;

namespace VäderApp.Services
{
    public class AddDataService : IAddDataService
    {
        private readonly EFContext _EFContext;
        public AddDataService(EFContext eFContext)
        {
            _EFContext = eFContext;
        }
        public async Task<bool> ConvertAndAdd(IBrowserFile browserFile)
        {
            var dataList = new List<WeatherData>();
            var filePath = Path.GetTempFileName();

            using (var stream = File.Create(filePath))
            {
                await browserFile.OpenReadStream(20000000).CopyToAsync(stream);
            }

            var data = await File.ReadAllLinesAsync(filePath);
            File.Delete(filePath);

            foreach (var item in data)
            {
                if (item != null)
                {
                    var splittedData = item.Split(",");

                    if (splittedData.Length == 4)
                    {
                        WeatherData weatherData = new WeatherData()
                        {
                            Date = DateTime.Parse(splittedData[0].Replace(" ", "T")),
                            Location = splittedData[1],
                            Temperature = double.Parse(splittedData[2], NumberFormatInfo.InvariantInfo),
                            Humidity = int.Parse(splittedData[3])
                        };
                        dataList.Add(weatherData);
                    } 
                }
            }
            await _EFContext.WeatherDatas.AddRangeAsync(dataList);
            await _EFContext.SaveChangesAsync();
            
            return true;
        }
    }
}
