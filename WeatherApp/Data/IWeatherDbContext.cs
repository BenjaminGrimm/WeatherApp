using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Data
{
    public interface IWeatherDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}