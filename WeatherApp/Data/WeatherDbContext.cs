using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Data
{
    public class WeatherDbContext : DbContext, IWeatherDbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Country> Countries { get; set; }
    }
}