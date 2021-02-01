using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VäderApp.Entities;

namespace VäderApp.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<WeatherData> WeatherDatas { get; set; }
    }
}
