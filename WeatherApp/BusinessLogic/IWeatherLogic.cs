using WeatherApp.Data;

namespace WeatherApp.BusinessLogic
{
    public interface IWeatherLogic
    {
        /// <summary>
        /// Weather summary of a country
        /// </summary>
        IEnumerable<WeatherRecord> CountryOverview(string country);
        /// <summary>
        /// Hourly weather forecast for the upcomming three days
        /// </summary>
        IEnumerable<WeatherRecord> ThreeDayForecastForLocation(string country, string location);
        /// <summary>
        /// Current weather at a location
        /// </summary>
        WeatherRecord WeatherForLocation(string country, string location);
    }
}