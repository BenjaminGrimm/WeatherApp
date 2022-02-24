using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherApp.BusinessLogic;
using WeatherApp.Data;
using WeatherApp.Exceptions;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController
    {
        private readonly IWeatherLogic weatherLogic;

        public WeatherController(IWeatherLogic weatherLogic)
        {
            this.weatherLogic = weatherLogic;
        }

        /// <summary>
        /// Shows the current weather overview of a country as a list of weather records for each location in the country
        /// </summary>
        /// <param name="country">County of interest</param>
        /// <returns>List of current weather records</returns>
        [HttpGet("overview/{country}")]
        public IEnumerable<WeatherRecord> GetCountryOverview([FromRoute] string country)
        {
            var overview = weatherLogic.CountryOverview(country);

            //TODO return 404 if appropriate. Changing the return type to ActionResult<IEnumerable<WeatherRecord>> does not work.

            return overview;
        }

        /// <summary>
        /// The current weather at a specific location
        /// </summary>
        /// <param name="country">County of interest</param>
        /// <param name="location">Location within the country</param>
        /// <returns>Current Weather</returns>
        [HttpGet("{country}/{location}")]
        public ActionResult<WeatherRecord> GetCurrentWeather([FromRoute] string country, [FromRoute] string location)
        {
            try
            {
                return weatherLogic.WeatherForLocation(country, location);
            }
            catch (CountryNotFoundException)
            {
                return new NotFoundObjectResult("Country not found");
            }
            catch (LocationNotFoundException)
            {
                return new NotFoundObjectResult("Location not found");
            }
            catch (NoRecordsFoundException)
            {
                return new NotFoundObjectResult("No records found!");
            }
            
        }

        /// <summary>
        /// Three days weather forecast at a specific location. Hourly prediction.
        /// </summary>
        /// <param name="country">County of interest</param>
        /// <param name="location">Location within the country</param>
        /// <returns>List of predicted weather records</returns>
        [HttpGet("forecast/{country}/{location}")]
        public IEnumerable<WeatherRecord> GetThreeDayForecast([FromRoute] string country, [FromRoute] string location)
        {
            var forecast = weatherLogic.ThreeDayForecastForLocation(country, location);

            //TODO return 404 if appropriate. Changing the return type to ActionResult<IEnumerable<WeatherRecord>> does not work.

            return forecast;
        }

    }
}
