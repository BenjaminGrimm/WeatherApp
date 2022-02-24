
namespace WeatherApp.Data
{
    public interface IWeatherRepository
    {
        IEnumerable<Country> Countries { get; }
        IEnumerable<Location> Locations { get; }
        IEnumerable<WeatherRecord> WeatherRecords { get; }
    }
}